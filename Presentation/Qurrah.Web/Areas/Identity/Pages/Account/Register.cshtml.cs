using System.Net;
using Qurrah.Business.Localization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Qurrah.Business.Logging;
using Qurrah.Entities;
using Qurrah.Integration.ServiceWrappers;
using DTOs = Qurrah.Integration.ServiceWrappers.DTOs.Authentication;
using LookupDTOs = Qurrah.Integration.ServiceWrappers.DTOs.Lookup;
using Qurrah.Integration.ServiceWrappers.Services.IServices;
using Qurrah.Utilities;
using Qurrah.Business.Lookup;
using Qurrah.Business.Extensions;
using System.ComponentModel;

namespace Qurrah.Web.Areas.Identity.Pages.Account

{
    public class RegisterModel : PageModel
    {
        #region Fields
        private readonly LanguageService _localization;
        private readonly IExceptionLogging _exceptionLogging;
        private readonly IUserAuthService _userAuthService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILookupManager _lookupManager;
        #endregion

        #region Ctor
        public RegisterModel(SignInManager<ApplicationUser> signInManager, LanguageService localization, ILookupManager lookupManager
                                                                         , IUserAuthService userAuthService, IExceptionLogging exceptionLogging)
        {
            _signInManager = signInManager;
            _localization = localization;
            _userAuthService = userAuthService;
            _exceptionLogging = exceptionLogging;
            _lookupManager = lookupManager;
        }
        #endregion

        #region Properties
        [BindProperty]
        public DTOs.RegistrationRequest RegistrationRequest { get; set; }
        public IEnumerable<SelectListItem> Genders { get; set; }
        public List<SelectListItem> UserTypes { get; set; }
        #endregion

        #region Handlers
        public async Task OnGetAsync()
        {
            RegistrationRequest = new();
            Genders = await GetAllGendersAsync();
            UserTypes = await GetUserTypesAsync();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _userAuthService.RegisterAsync<APIResponse>(RegistrationRequest);
                    if (response?.IsSuccess != true && response.StatusCode == HttpStatusCode.BadRequest)
                        if (null != response.Errors?.FirstOrDefault()?.FirstOrDefault() && response.Errors.First().First().ToLower().Contains("username"))
                            ModelState.AddModelError(string.Empty, _localization.GetLocalizedString("Messages.ErrorMessages.UserAlreadyExists"));
                        else
                            ModelState.AddModelError(string.Empty, _localization.GetLocalizedString("Validation.GeneralErrorMessage"));

                    else if (null == response || null == response.Result || (!response.IsSuccess && response.StatusCode == HttpStatusCode.InternalServerError))
                        HttpContext.Session.SetString(Business.Constants.Session_Error, _localization.GetLocalizedString("Messages.ErrorMessages.GeneralError"));
                    else
                    {
                        var registrationResponse = JsonConvert.DeserializeObject<DTOs.RegistrationResponse>(Convert.ToString(response.Result));
                        if (registrationResponse.Succeeded)
                        {
                            var loginResponse = await _userAuthService.LoginAsync<APIResponse>(new DTOs.LoginRequest
                            {
                                UserName = RegistrationRequest.UserName,
                                Password = RegistrationRequest.Password,
                            });

                            if (null != loginResponse)
                            {
                                var loginResult = JsonConvert.DeserializeObject<DTOs.LoginResponse>(Convert.ToString(loginResponse.Result));
                                if (null != loginResult)
                                {
                                    await _signInManager.PasswordSignInAsync(RegistrationRequest.UserName, RegistrationRequest.Password, false, lockoutOnFailure: false);

                                    HttpContext.Response.Cookies.Append(Business.Constants.JWTTokenName
                                                                                    , loginResult.Token
                                                                                    , new CookieOptions
                                                                                    {
                                                                                        Expires = DateTime.Now.AddMinutes(20)
                                                                                    });
                                }
                            }

                            HttpContext.Session.SetString(Business.Constants.Session_Success, _localization.GetLocalizedString("Messages.SuccessMessages.RegisterUserSuccess"));
                            return RedirectToAction("Index", "Home", new { area = "Public" });
                        }
                        else
                            HttpContext.Session.SetString(Business.Constants.Session_Error, _localization.GetLocalizedString("Messages.ErrorMessages.GeneralError"));
                    }
                }
            }
            catch (Exception ex)
            {
                _exceptionLogging.Log(ex);
            }
            Genders = await GetAllGendersAsync();
            UserTypes = await GetUserTypesAsync();
            return Page();
        }
        #endregion

        #region Utilities
        public async Task<List<SelectListItem>> GetUserTypesAsync()
        {
            List<LookupDTOs.LookupInfo> userTypes = null;
            try
            {
                var result = await _lookupManager.GetAllUserTypesAsync(Thread.CurrentThread.CurrentCulture.Name);
                if (result.ActionResult == Business.ActionResult.Success)
                    userTypes = result.Result as List<LookupDTOs.LookupInfo>;
                else
                {
                    if (result?.ActionResult == Business.ActionResult.InternalServerError && result.ErrorMessages?.Any() == true)
                        _exceptionLogging.Log(result.ErrorMessages.Concatenate());
                    else
                        _exceptionLogging.Log("An error occured while loading User Types!");
                }
            }
            catch (Exception ex)
            {
                _exceptionLogging.Log(ex);
            }
            return userTypes?.Select(g => new SelectListItem(g.Text, g.Id.ToString()))
                             .Where(ut => int.Parse(ut.Value) != (int)UserTypeId.Administrator && int.Parse(ut.Value) != (int)UserTypeId.CenterApprover)
                             .ToList() ?? new List<SelectListItem>();
        }
        public async Task<List<SelectListItem>> GetAllGendersAsync()
        {
            List<LookupDTOs.LookupInfo> genders = null;
            try
            {
                var result = await _lookupManager.GetAllGendersAsync(Thread.CurrentThread.CurrentCulture.Name);
                if (result.ActionResult == Business.ActionResult.Success)
                    genders = result.Result as List<LookupDTOs.LookupInfo>;
                else
                {
                    if (result?.ActionResult == Business.ActionResult.InternalServerError && result.ErrorMessages?.Any() == true)
                        _exceptionLogging.Log(result.ErrorMessages.Concatenate());
                    else
                        _exceptionLogging.Log("An error occured while loading Genders!");
                }
            }
            catch (Exception ex)
            {
                _exceptionLogging.Log(ex);
            }
            return genders?.Select(g => new SelectListItem(g.Text, g.Id.ToString())).ToList() ?? new List<SelectListItem>();
        }
        #endregion
    }
}