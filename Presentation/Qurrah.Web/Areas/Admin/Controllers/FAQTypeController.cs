using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Qurrah.Business.Extensions;
using Qurrah.Business.FAQ;
using Qurrah.Business.Localization;
using Qurrah.Business.Localization.Entities;
using Qurrah.Business.Logging;
using Qurrah.Business.UserAuth;
using Qurrah.Integration.ServiceWrappers;
using Qurrah.Integration.ServiceWrappers.Services.IServices;
using Qurrah.Web.Areas.Admin.Models;
using Qurrah.Web.Utilities;
using System.Net;
using ActionResult = Microsoft.AspNetCore.Mvc.ActionResult;
using FAQDTOs = Qurrah.Integration.ServiceWrappers.DTOs.FAQ;
using LocalizationDTOs = Qurrah.Integration.ServiceWrappers.DTOs.Localization;

namespace Qurrah.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator")]
    public class FAQTypeController : Controller
    {
        #region Fields
        private readonly IFAQTypeManager _faqTypeManager;
        private readonly ILocalizatonManager _localizatonManager;
        private readonly LanguageService _localization;
        private readonly IExceptionLogging _exceptionLogging;
        private readonly IMapper _mapper;
        private readonly ILocalizationUtility _localizationUtility;
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
        public FAQTypeController(IMapper mapper, ILocalizationUtility localizationUtility, ILocalizatonManager localizatonManager, IFAQTypeManager faqTypeManager, LanguageService localization, IExceptionLogging exceptionLogging)
        {
            _mapper = mapper;
            _localizatonManager = localizatonManager;
            _faqTypeManager = faqTypeManager;
            _localization = localization;
            _exceptionLogging = exceptionLogging;
            _localizationUtility = localizationUtility;
        }
        #endregion

        #region Actions
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            IEnumerable<FAQDTOs.FAQType> faqTypes = null;
            try
            {
                var result = await _faqTypeManager.GetAllAsync(AuthTokenValue);
                if (result?.ActionResult == Business.ActionResult.Success)
                    faqTypes = result.Result as List<FAQDTOs.FAQType>;
                else
                {
                    HttpContext.Session.SetString(Business.Constants.Session_Error, _localization.GetLocalizedString("Messages.ErrorMessages.GeneralError"));
                    
                    if (result?.ActionResult == Business.ActionResult.InternalServerError && result.ErrorMessages?.Any() == true)
                        _exceptionLogging.Log(result.ErrorMessages.Concatenate());
                    else
                        _exceptionLogging.Log("An error occured while loading FAQ Types!");
                }
            }
            catch (Exception ex)
            {
                HttpContext.Session.SetString(Business.Constants.Session_Error, _localization.GetLocalizedString("Messages.ErrorMessages.GeneralError"));
                _exceptionLogging.Log(ex);
            }
            return View(faqTypes ?? new List<FAQDTOs.FAQType>());
        }

        [HttpGet]
        public async Task<ActionResult> View(int id)
        {
            try
            {
                var result = await _faqTypeManager.GetAsync(id, AuthTokenValue);
                if (result.ActionResult == Business.ActionResult.Success)
                {
                    var faqTypeWithLocalizedProps = result.Result as FAQDTOs.FAQTypeWithLocalizedProperties;
                    
                    FAQTypeVM faqTypeVM = new();
                    faqTypeVM.Locales = await _localizationUtility.GetLocales();
                    faqTypeVM.FAQType = faqTypeWithLocalizedProps.FAQType;

                    var locGroupsResult = _faqTypeManager.PopulateLocalizedPropertyGroups(faqTypeWithLocalizedProps.LocalizedProperties, faqTypeVM.Locales, true);
                    var localizedPropertyGroups = locGroupsResult.Result as List<LocalizedPropertyGroup>;

                    faqTypeVM.LocalizedPropertyGroups = _mapper.Map<List<LocalizedPropertyGroupVM>>(localizedPropertyGroups);
                    
                    return View(faqTypeVM);
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

        [HttpGet]
        public async Task<ActionResult> Create()
        {
            FAQTypeVM faqTypeCreateViewModel = new();
            try
            {
                faqTypeCreateViewModel.Locales = await _localizationUtility.GetLocales();

                var result = _faqTypeManager.PopulateDefaultLocalizedPropertyGroups(faqTypeCreateViewModel.Locales);
                if (result.ActionResult == Business.ActionResult.Success)
                {
                    var localizedProps = result.Result as List<LocalizedPropertyGroup>;
                    faqTypeCreateViewModel.LocalizedPropertyGroups = _mapper.Map<List<LocalizedPropertyGroupVM>>(localizedProps);
                }
            }
            catch (Exception ex)
            {
                _exceptionLogging.Log(ex);
            }

            return View(faqTypeCreateViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(FAQTypeVM faqTypeViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    List<LocalizationDTOs.LocalizedProperty> localizedProperties = new();
                    faqTypeViewModel.LocalizedPropertyGroups
                                          ?.ForEach(group => localizedProperties.AddRange(group.LocalizedProperties
                                                                                .Where(lp => !string.IsNullOrWhiteSpace(lp.LocaleValue?.Trim()))));
                    FAQDTOs.FAQTypeWithLocalizedProperties faqTypeWithLocalizedProps = new FAQDTOs.FAQTypeWithLocalizedProperties
                    {
                        FAQType = faqTypeViewModel.FAQType,
                        LocalizedProperties = localizedProperties
                    };

                    var apiResult = await _faqTypeManager.CreateAsync(faqTypeWithLocalizedProps, AuthTokenValue);

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
                            _exceptionLogging.Log("An error occured while saving faq type!");
                    }
                }

                faqTypeViewModel.Locales = await _localizationUtility.GetLocales();
            }
            catch (Exception ex)
            {
                _exceptionLogging.Log(ex);
                HttpContext.Session.SetString(Business.Constants.Session_Error, _localization.GetLocalizedString("Messages.ErrorMessages.GeneralError"));
            }

            return View(faqTypeViewModel);
        }

        [HttpGet]
        public async Task<ActionResult> Update(int id)
        {
            try
            {
                var result = await _faqTypeManager.GetAsync(id, AuthTokenValue);
                if (result.ActionResult == Business.ActionResult.Success)
                {
                    var faqTypeWithLocalizedProps = result.Result as FAQDTOs.FAQTypeWithLocalizedProperties;

                    FAQTypeVM faqTypeVM = new();
                    faqTypeVM.Locales = await _localizationUtility.GetLocales();
                    faqTypeVM.FAQType = faqTypeWithLocalizedProps.FAQType;

                    var locGroupsResult = _faqTypeManager.PopulateLocalizedPropertyGroups(faqTypeWithLocalizedProps.LocalizedProperties, faqTypeVM.Locales, false);
                    var localizedPropertyGroups = locGroupsResult.Result as List<LocalizedPropertyGroup>;

                    faqTypeVM.LocalizedPropertyGroups = _mapper.Map<List<LocalizedPropertyGroupVM>>(localizedPropertyGroups);

                    return View(faqTypeVM);
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
        public async Task<ActionResult> Update(FAQTypeVM faqTypeViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    List<LocalizationDTOs.LocalizedProperty> localizedProperties = new();
                    faqTypeViewModel.LocalizedPropertyGroups
                                          ?.ForEach(group => localizedProperties.AddRange(group.LocalizedProperties));
                    FAQDTOs.FAQTypeWithLocalizedProperties faqTypeWithLocalizedProps = new FAQDTOs.FAQTypeWithLocalizedProperties
                    {
                        FAQType = faqTypeViewModel.FAQType,
                        LocalizedProperties = localizedProperties
                    };

                    var apiResult = await _faqTypeManager.UpdateAsync(faqTypeWithLocalizedProps, AuthTokenValue);

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
                            _exceptionLogging.Log("An error occured while saving faq type!");
                    }
                }

                faqTypeViewModel.Locales = await _localizationUtility.GetLocales();
            }
            catch (Exception ex)
            {
                _exceptionLogging.Log(ex);
                HttpContext.Session.SetString(Business.Constants.Session_Error, _localization.GetLocalizedString("Messages.ErrorMessages.GeneralError"));
            }

            return View(faqTypeViewModel);
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var result = await _faqTypeManager.DeleteAsync(id, AuthTokenValue);
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
    }
}