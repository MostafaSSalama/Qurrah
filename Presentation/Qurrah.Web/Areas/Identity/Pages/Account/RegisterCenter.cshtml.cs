using System.Net;
using AutoMapper;
using Localization.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Qurrah.Business.Logging;
using Qurrah.Entities;
using Qurrah.Entities.NoMapping;
using Qurrah.Integration.ServiceWrappers;
using Qurrah.Integration.ServiceWrappers.DTOs.Authentication;
using Qurrah.Integration.ServiceWrappers.Services.IServices;
using Qurrah.Utilities;

namespace Qurrah.Web.Areas.Identity.Pages.Account

{
    public class RegisterCenter : PageModel
    {
        #region Fields
        private readonly LanguageService _localization;
        private readonly IExceptionLogging _exceptionLogging;
        private readonly IMapper _mapper;
        private readonly IUserAuthService _userAuthService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        #endregion

        #region Ctor
        public RegisterCenter(SignInManager<ApplicationUser> signInManager, LanguageService localization, IMapper mapper
                                                                          , IUserAuthService userAuthService, IExceptionLogging exceptionLogging)
        {
            _signInManager = signInManager;
            _localization = localization;
            _mapper = mapper;
            _userAuthService = userAuthService;
            _exceptionLogging = exceptionLogging;
        }
        #endregion

        #region Properties
        [BindProperty]
        public CenterUserRegistrationRequestDTO RegistrationRequest { get; set; }
        public IEnumerable<SelectListItem> Genders
        {
            get
            {
                return GenderUtility.GetAllGenders().Select(g => new SelectListItem(_localization.CurrentLanguage == SupportedLanguage.English ? g.NameEn : g.NameAr, g.Id.ToString()));
            }
        }
        #endregion

        #region Handlers
        public async Task OnGetAsync()
        {
            RegistrationRequest = new();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _userAuthService.RegisterAsync<APIResponse>(RegistrationRequest);
                    if (response?.IsSuccess != true && response.StatusCode == HttpStatusCode.BadRequest)
                    {
                        if (null != response.Errors?.FirstOrDefault()?.FirstOrDefault() && response.Errors.First().First().ToLower().Contains("username"))
                            ModelState.AddModelError(string.Empty, _localization.GetLocalizedString("Messages.ErrorMessages.UserAlreadyExists"));
                        else
                            ModelState.AddModelError(string.Empty, _localization.GetLocalizedString("Validation.GeneralErrorMessage"));

                        return Page();
                    }
                    else if (null == response || null == response.Result || (!response.IsSuccess && response.StatusCode == HttpStatusCode.InternalServerError))
                    {
                        HttpContext.Session.SetString(Constants.Session_Error, _localization.GetLocalizedString("Messages.ErrorMessages.GeneralError"));
                        return Page();
                    }
                    else
                    {
                        var result = JsonConvert.DeserializeObject<RegistrationResponse>(Convert.ToString(response.Result));
                        var registrationResponse = _mapper.Map<RegistrationResponseDTO>(result);
                        if (registrationResponse.Succeeded)
                        {
                            var loginResponse = await _userAuthService.LoginAsync<APIResponse>(new LoginRequestDTO
                            {
                                UserName = RegistrationRequest.CenterOwner.UserName,
                                Password = RegistrationRequest.CenterOwner.Password,
                            });

                            if (null != loginResponse)
                            {
                                var loginResult = JsonConvert.DeserializeObject<LoginResponse>(Convert.ToString(loginResponse.Result));
                                var loginResponseDTO = _mapper.Map<LoginResponseDTO>(loginResult);
                                if (null != loginResponseDTO)
                                {
                                    await _signInManager.PasswordSignInAsync(RegistrationRequest.CenterOwner.UserName, RegistrationRequest.CenterOwner.Password, false, lockoutOnFailure: false);
                                    HttpContext.Session.SetString(Constants.Session_AuthTokenName, loginResponseDTO.Token);
                                }
                            }

                            HttpContext.Session.SetString(Constants.Session_Success, _localization.GetLocalizedString("Messages.SuccessMessages.RegisterUserSuccess"));

                            return RedirectToAction("Index", "Home", new { area = "Public" });
                        }
                        else
                            HttpContext.Session.SetString(Constants.Session_Error, _localization.GetLocalizedString("Messages.ErrorMessages.GeneralError"));
                    }
                }
            }
            catch (Exception ex)
            {
                _exceptionLogging.Log(ex);
            }
            return Page();
        }
        #endregion
    }
}