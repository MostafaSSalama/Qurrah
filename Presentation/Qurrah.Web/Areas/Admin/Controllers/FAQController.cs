using Qurrah.Business.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Qurrah.Business.Logging;
using Qurrah.Integration.ServiceWrappers.Services.IServices;
using Qurrah.Web.Areas.Admin.Models;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Qurrah.Integration.ServiceWrappers;
using FAQDTOs = Qurrah.Integration.ServiceWrappers.DTOs.FAQ;
using LocalizationDTOs = Qurrah.Integration.ServiceWrappers.DTOs.Localization;
using Qurrah.Business.FAQ;
using Qurrah.Business.Extensions;
using Qurrah.Web.Utilities;
using Qurrah.Business.Localization.Entities;
using AutoMapper;
using Qurrah.Business.UserAuth;

namespace Qurrah.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator")]
    public class FAQController : Controller
    {
        #region Fields
        private readonly IFAQManager _faqManager;
        private readonly IFAQTypeManager _faqTypeManager;
        private readonly ILocalizatonManager _localizatonManager;
        private readonly ILocalizationUtility _localizationUtility;
        private readonly IMapper _mapper;
        private LanguageService _localization;
        IExceptionLogging _exceptionLogging;
        #endregion

        #region Properties
        public string AuthTokenValue
        {
            get
            {
                return UserManager.JWTTokenValue;
            }
        }
        #endregion

        #region Ctor
        public FAQController(IMapper mapper, ILocalizatonManager localizatonManager, IFAQManager faqManager, IFAQTypeManager faqTypeManager, LanguageService localization, IExceptionLogging exceptionLogging, ILocalizationUtility localizationUtility)
        {
            _localizatonManager = localizatonManager;
            _faqManager = faqManager;
            _faqTypeManager = faqTypeManager;
            _localization = localization;
            _exceptionLogging = exceptionLogging;
            _localizationUtility = localizationUtility;
            _mapper = mapper;
        }
        #endregion

        #region Actions
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            IEnumerable<FAQDTOs.FAQ> faqTypes = null;
            try
            {
                var result = await _faqManager.GetAllAsync(AuthTokenValue);
                if (result.ActionResult == Business.ActionResult.Success)
                    faqTypes = result.Result as List<FAQDTOs.FAQ>;
                else
                {
                    HttpContext.Session.SetString(Business.Constants.Session_Error, _localization.GetLocalizedString("Messages.ErrorMessages.GeneralError"));

                    if (result?.ActionResult == Business.ActionResult.InternalServerError && result.ErrorMessages?.Any() == true)
                        _exceptionLogging.Log(result.ErrorMessages.Concatenate());
                    else
                        _exceptionLogging.Log("An error occured while loading FAQs!");
                }
            }
            catch (Exception ex)
            {
                HttpContext.Session.SetString(Business.Constants.Session_Error, _localization.GetLocalizedString("Messages.ErrorMessages.GeneralError"));
                _exceptionLogging.Log(ex);
            }
            return View(faqTypes ?? new List<FAQDTOs.FAQ>());
        }

        [HttpGet]
        public async Task<ActionResult> View(int id)
        {
            try
            {
                var result = await _faqManager.GetAsync(id, AuthTokenValue);
                if (result.ActionResult == Business.ActionResult.Success)
                {
                    var faqWithLocalizedProps = result.Result as FAQDTOs.FAQWithLocalizedProperties;

                    FAQVM faqVM = new();
                    faqVM.Locales = await _localizationUtility.GetLocales();
                    faqVM.FAQ = faqWithLocalizedProps.FAQ;

                    var locGroupsResult = _faqManager.PopulateLocalizedPropertyGroups(faqWithLocalizedProps.LocalizedProperties, faqVM.Locales, true);
                    var localizedPropertyGroups = locGroupsResult.Result as List<LocalizedPropertyGroup>;

                    faqVM.LocalizedPropertyGroups = _mapper.Map<List<LocalizedPropertyGroupVM>>(localizedPropertyGroups);

                    return View(faqVM);
                }
                else if (result.ActionResult == Business.ActionResult.ResourceNotFound)
                    return NotFound();
                else
                {
                    HttpContext.Session.SetString(Business.Constants.Session_Error, _localization.GetLocalizedString("Messages.ErrorMessages.GeneralError"));

                    if (result?.ActionResult == Business.ActionResult.InternalServerError && result.ErrorMessages?.Any() == true)
                        _exceptionLogging.Log(result.ErrorMessages.Concatenate());
                    else
                        _exceptionLogging.Log("An error occured while loading FAQs!");
                }
            }
            catch (Exception ex)
            {
                _exceptionLogging.Log(ex);
                HttpContext.Session.SetString(Business.Constants.Session_Error, _localization.GetLocalizedString("Messages.ErrorMessages.GeneralError"));
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Create()
        {
            FAQVM faqViewModel = new FAQVM();
            try
            {
                faqViewModel.Locales = await _localizationUtility.GetLocales();
                
                var result = _faqManager.PopulateDefaultLocalizedPropertyGroups(faqViewModel.Locales);
                if (result.ActionResult == Business.ActionResult.Success)
                {
                    var localizedProps = result.Result as List<LocalizedPropertyGroup>;
                    faqViewModel.LocalizedPropertyGroups = _mapper.Map<List<LocalizedPropertyGroupVM>>(localizedProps);
                }

                faqViewModel.FAQTypes = await GetFAQTypes();
            }
            catch (Exception ex)
            {
                _exceptionLogging.Log(ex);
            }
            return View(faqViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(FAQVM faqViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    List<LocalizationDTOs.LocalizedProperty> localizedProperties = new();
                    faqViewModel.LocalizedPropertyGroups
                                          ?.ForEach(group => localizedProperties.AddRange(group.LocalizedProperties
                                                                                .Where(lp => !string.IsNullOrWhiteSpace(lp.LocaleValue?.Trim()))));
                    FAQDTOs.FAQWithLocalizedProperties faqTypeWithLocalizedProps = new FAQDTOs.FAQWithLocalizedProperties
                    {
                        FAQ = faqViewModel.FAQ,
                        LocalizedProperties = localizedProperties
                    };

                    var apiResult = await _faqManager.CreateAsync(faqTypeWithLocalizedProps, AuthTokenValue);

                    if (apiResult?.ActionResult == Business.ActionResult.Success)
                    {
                        HttpContext.Session.SetString(Business.Constants.Session_Success, _localization.GetLocalizedString("Messages.SuccessMessages.SaveGeneralSuccess"));
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        HttpContext.Session.SetString(Business.Constants.Session_Error, _localization.GetLocalizedString("Messages.ErrorMessages.GeneralError"));

                        if (apiResult?.ActionResult == Business.ActionResult.InternalServerError && apiResult.ErrorMessages?.Any() == true)
                            _exceptionLogging.Log(apiResult.ErrorMessages.Concatenate());
                        else
                            _exceptionLogging.Log("An error occured while saving faq!");
                    }
                }

                faqViewModel.Locales = await _localizationUtility.GetLocales();
                faqViewModel.FAQTypes = await GetFAQTypes();
            }
            catch (Exception ex)
            {
                _exceptionLogging.Log(ex);
                HttpContext.Session.SetString(Business.Constants.Session_Error, _localization.GetLocalizedString("Messages.ErrorMessages.GeneralError"));
            }
            return View(faqViewModel);
        }

        [HttpGet]
        public async Task<ActionResult> Update(int id)
        {
            try
            {
                var result = await _faqManager.GetAsync(id, AuthTokenValue);
                if (result.ActionResult == Business.ActionResult.Success)
                {
                    var faqWithLocalizedProps = result.Result as FAQDTOs.FAQWithLocalizedProperties;

                    FAQVM faqVM = new();
                    faqVM.Locales = await _localizationUtility.GetLocales();
                    faqVM.FAQ = faqWithLocalizedProps.FAQ;

                    var locGroupsResult = _faqManager.PopulateLocalizedPropertyGroups(faqWithLocalizedProps.LocalizedProperties, faqVM.Locales, false);
                    var localizedPropertyGroups = locGroupsResult.Result as List<LocalizedPropertyGroup>;
                    
                    faqVM.LocalizedPropertyGroups = _mapper.Map<List<LocalizedPropertyGroupVM>>(localizedPropertyGroups);
                    faqVM.FAQTypes = await GetFAQTypes();

                    return View(faqVM);
                }
                else if (result.ActionResult == Business.ActionResult.ResourceNotFound)
                    return NotFound();
                else
                {
                    HttpContext.Session.SetString(Business.Constants.Session_Error, _localization.GetLocalizedString("Messages.ErrorMessages.GeneralError"));

                    if (result?.ActionResult == Business.ActionResult.InternalServerError && result.ErrorMessages?.Any() == true)
                        _exceptionLogging.Log(result.ErrorMessages.Concatenate());
                    else
                        _exceptionLogging.Log("An error occured while loading FAQ Type!");
                }
            }
            catch (Exception ex)
            {
                _exceptionLogging.Log(ex);
                HttpContext.Session.SetString(Business.Constants.Session_Error, _localization.GetLocalizedString("Messages.ErrorMessages.GeneralError"));
            }

            return RedirectToAction("Index");
        }

        [HttpPost(Name = "Update")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(FAQVM faqViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    List<LocalizationDTOs.LocalizedProperty> localizedProperties = new();
                    faqViewModel.LocalizedPropertyGroups
                                          ?.ForEach(group => localizedProperties.AddRange(group.LocalizedProperties));
                    FAQDTOs.FAQWithLocalizedProperties faqWithLocalizedProps = new FAQDTOs.FAQWithLocalizedProperties
                    {
                        FAQ = faqViewModel.FAQ,
                        LocalizedProperties = localizedProperties
                    };

                    var apiResult = await _faqManager.UpdateAsync(faqWithLocalizedProps, AuthTokenValue);

                    if (apiResult?.ActionResult == Business.ActionResult.Success)
                    {
                        HttpContext.Session.SetString(Business.Constants.Session_Success, _localization.GetLocalizedString("Messages.SuccessMessages.SaveGeneralSuccess"));
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        HttpContext.Session.SetString(Business.Constants.Session_Error, _localization.GetLocalizedString("Messages.ErrorMessages.GeneralError"));

                        if (apiResult?.ActionResult == Business.ActionResult.InternalServerError && apiResult.ErrorMessages?.Any() == true)
                            _exceptionLogging.Log(apiResult.ErrorMessages.Concatenate());
                        else
                            _exceptionLogging.Log("An error occured while saving faq!");
                    }
                }

                faqViewModel.Locales = await _localizationUtility.GetLocales();
                faqViewModel.FAQTypes = await GetFAQTypes();
            }
            catch (Exception ex)
            {
                _exceptionLogging.Log(ex);
                HttpContext.Session.SetString(Business.Constants.Session_Error, _localization.GetLocalizedString("Messages.ErrorMessages.GeneralError"));
            }

            return View(faqViewModel);
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var result = await _faqManager.DeleteAsync(id, AuthTokenValue);
                if (result.ActionResult == Business.ActionResult.Success)
                {
                    HttpContext.Session.SetString(Business.Constants.Session_Success, _localization.GetLocalizedString("Messages.SuccessMessages.DeleteGeneralSuccess"));
                    return Json(new { success = true });
                }
                else if (result.ActionResult == Business.ActionResult.ResourceNotFound)
                    return NotFound();
                else
                {
                    HttpContext.Session.SetString(Business.Constants.Session_Error, _localization.GetLocalizedString("Messages.ErrorMessages.GeneralError"));

                    if (result?.ActionResult == Business.ActionResult.InternalServerError && result.ErrorMessages?.Any() == true)
                        _exceptionLogging.Log(result.ErrorMessages.Concatenate());
                    else
                        _exceptionLogging.Log("An error occured while deleting FAQ Type!");
                }
            }
            catch (Exception ex)
            {
                _exceptionLogging.Log(ex);
            }

            HttpContext.Session.SetString(Business.Constants.Session_Error, _localization.GetLocalizedString("Messages.ErrorMessages.GeneralError"));
            return Json(new { success = false });
        }
        #endregion

        #region Utilities
        public async Task<IEnumerable<SelectListItem>> GetFAQTypes()
        {
            IEnumerable<FAQDTOs.FAQType> faqTypes = null;
            try
            {
                var result = await _faqTypeManager.GetAllAsync(AuthTokenValue);
                if (result?.ActionResult == Business.ActionResult.Success)
                    faqTypes = result.Result as List<FAQDTOs.FAQType>;
                else
                {
                    if (result?.ActionResult == Business.ActionResult.InternalServerError && result.ErrorMessages?.Any() == true)
                        _exceptionLogging.Log(result.ErrorMessages.Concatenate());
                    else
                        _exceptionLogging.Log("An error occured while loading FAQ Types!");
                }
            }
            catch (Exception ex)
            {
                _exceptionLogging.Log(ex);
            }
            return (faqTypes ?? new List<FAQDTOs.FAQType>()).Select(t => new SelectListItem(t.Name, t.Id.ToString())).ToList();
        }
        #endregion
    }
}