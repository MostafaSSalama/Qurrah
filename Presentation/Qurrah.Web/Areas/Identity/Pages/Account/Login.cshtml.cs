﻿using Qurrah.Business.Localization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Qurrah.Business.Logging;
using Qurrah.Entities;
using Qurrah.Integration.ServiceWrappers;
using DTOs = Qurrah.Integration.ServiceWrappers.DTOs.Authentication;
using Qurrah.Integration.ServiceWrappers.Services.IServices;
using System.Net;

namespace Qurrah.Web.Areas.Identity.Pages.Account
{
    public class LoginModel : PageModel
    {
        #region Fields
        private readonly LanguageService _localization;
        private readonly IExceptionLogging _exceptionLogging;
        private readonly IInfoLogging _infoLogging;
        private readonly IUserAuthService _userAuthService;
        private readonly IConfiguration _configuration;
        private readonly SignInManager<ApplicationUser> _signInManager;
        #endregion

        #region Ctor
        public LoginModel(SignInManager<ApplicationUser> signInManager, IExceptionLogging exceptionLogging
                                                                      , IInfoLogging infoLogging, LanguageService localization
                                                                      , IUserAuthService userAuthService
                                                                      , IConfiguration configuration)
        {
            _signInManager = signInManager;
            _exceptionLogging = exceptionLogging;
            _infoLogging = infoLogging;
            _localization = localization;
            _userAuthService = userAuthService;
            _configuration = configuration;
        }
        #endregion

        #region Properties
        [BindProperty]
        public DTOs.LoginRequest LoginRequest { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }
        #endregion

        #region Actions
        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
                ModelState.AddModelError(string.Empty, ErrorMessage);

            returnUrl ??= Url.Content("~/");

            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _userAuthService.LoginAsync<APIResponse>(LoginRequest);
                    if (response?.IsSuccess != true && response.StatusCode == HttpStatusCode.BadRequest)
                    {
                        if (null != response.Errors?.FirstOrDefault()?.FirstOrDefault() && response.Errors.First().First().ToLower().Contains("username"))
                            ModelState.AddModelError(string.Empty, _localization.GetLocalizedString("Messages.ErrorMessages.CredentialsNotCorrect"));
                        else
                            ModelState.AddModelError(string.Empty, _localization.GetLocalizedString("Validation.GeneralErrorMessage"));
                    }
                    else if (null == response || null == response.Result || (!response.IsSuccess && response.StatusCode == HttpStatusCode.InternalServerError))
                        HttpContext.Session.SetString(Business.Constants.Session_Error, _localization.GetLocalizedString("Messages.ErrorMessages.GeneralError"));
                    else
                    {
                        var loginResult = JsonConvert.DeserializeObject<DTOs.LoginResponse>(Convert.ToString(response.Result));
                        if (loginResult.UserExists)
                        {
                            if (!string.IsNullOrWhiteSpace(loginResult.Token))
                            {
                                await _signInManager.PasswordSignInAsync(LoginRequest.UserName, LoginRequest.Password, LoginRequest.RememberMe, lockoutOnFailure: false);

                                HttpContext.Response.Cookies.Append(Business.Constants.JWTTokenName
                                    , loginResult.Token
                                    , new CookieOptions
                                    {
                                        Expires = LoginRequest.RememberMe ? DateTime.Now.AddDays(int.Parse(_configuration.GetValue<string>("Authentication:JWTLifetimeInDays")))
                                                                          : DateTime.Now.AddMinutes(int.Parse(_configuration.GetValue<string>("Authentication:JWTLifetimeInMinutes")))
                                    });

                                returnUrl ??= Url.Content("~/");
                                return LocalRedirect(returnUrl);
                            }
                            else
                                HttpContext.Session.SetString(Business.Constants.Session_Error, _localization.GetLocalizedString("Messages.ErrorMessages.GeneralError"));
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
            return Page();
        }
        #endregion
    }
}