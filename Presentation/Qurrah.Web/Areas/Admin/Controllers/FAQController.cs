using AutoMapper;
using Localization.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Qurrah.Business.Logging;
using Qurrah.Business.Logging.Logger;
using Qurrah.Integration.ServiceWrappers.Services.IServices;
using Qurrah.Models.Integration;
using Qurrah.Models.Integration.DTOs.FAQ;
using Qurrah.Models.Integration.DTOs.FAQType;
using Qurrah.Web.Areas.Admin.Models;
using System.Net;

namespace Qurrah.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FAQController : Controller
    {
        #region Fields
        private readonly IFAQService _faqService;
        IFAQTypeService _faqTypeService;
        private LanguageService _localization;
        IExceptionLogging _exceptionLogging;
        private readonly IMapper _mapper;
        #endregion

        #region Ctor
        public FAQController(IFAQService faqService, IFAQTypeService faqTypeService, IMapper mapper, LanguageService localization, IExceptionLogging exceptionLogging)
        {
            _faqService = faqService;
            _faqTypeService = faqTypeService;
            _localization = localization;
            _exceptionLogging = exceptionLogging;
            _mapper = mapper;
        }
        #endregion

        #region Actions
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            IEnumerable<FAQDTO> faqs = null;
            try
            {
                var response = await _faqService.GetAllAsync<APIResponse>();
                if (response?.IsSuccess == true && null != response.Result && response.StatusCode == HttpStatusCode.OK)
                    faqs = JsonConvert.DeserializeObject<IEnumerable<FAQDTO>>(Convert.ToString(response.Result));
            }
            catch (Exception ex)
            {
                HttpContext.Session.SetString("Error", _localization.GetLocalizedString("Messages.ErrorMessages.GeneralError"));
                _exceptionLogging.Log(ex);
            }
            return View(faqs ?? new List<FAQDTO>());
        }

        [HttpGet]
        public async Task<ActionResult> View(int id)
        {
            try
            {
                var response = await _faqService.GetAsync<APIResponse>(id);
                if (response?.IsSuccess == true && response.StatusCode == HttpStatusCode.OK && null != response.Result)
                {
                    var faq = JsonConvert.DeserializeObject<FAQDTO>(Convert.ToString(response.Result));
                    return View(faq);
                }
            }
            catch (Exception ex)
            {
                _exceptionLogging.Log(ex);
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<ActionResult> Create()
        {
            FAQCreateViewModel faqCreateViewModel = new FAQCreateViewModel();
            try
            {
                faqCreateViewModel.FAQTypes = await GetFAQTypes();
            }
            catch(Exception ex)
            {
                _exceptionLogging.Log(ex);
            }
            return View(faqCreateViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(FAQCreateViewModel faqCreateViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _faqService.CreateAsync<APIResponse>(faqCreateViewModel.FAQ);
                    if (response?.IsSuccess == true && response.StatusCode == HttpStatusCode.Created)
                    {
                        var faq = JsonConvert.DeserializeObject<FAQDTO>(Convert.ToString(response.Result));

                        HttpContext.Session.SetString("Success", _localization.GetLocalizedString("Messages.SuccessMessages.SaveGeneralSuccess"));
                        return RedirectToAction("Index", new { id = faq.Id });
                    }
                }
            }
            catch (Exception ex)
            {
                faqCreateViewModel.FAQTypes = await GetFAQTypes();
                _exceptionLogging.Log(ex);
            }
            return View(faqCreateViewModel);
        }

        [HttpGet]
        public async Task<ActionResult> Update(int id)
        {
            try
            {
                FAQUpdateViewModel faqUpdateViewModel = new();

                var response = await _faqService.GetAsync<APIResponse>(id);
                if (response?.IsSuccess == true && response.StatusCode == HttpStatusCode.OK && null != response.Result)
                {
                    faqUpdateViewModel.FAQ = JsonConvert.DeserializeObject<FAQUpdateDTO>(Convert.ToString(response.Result));
                    faqUpdateViewModel.FAQTypes = await GetFAQTypes();

                    return View(faqUpdateViewModel);
                }
            }
            catch (Exception ex)
            {
                _exceptionLogging.Log(ex);
            }
            return NotFound();
        }

        [HttpPost(Name = "Update")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(FAQUpdateViewModel faqUpdateViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _faqService.UpdateAsync<APIResponse>(faqUpdateViewModel.FAQ);
                    if (response?.IsSuccess == true && response.StatusCode == HttpStatusCode.NoContent)
                    {
                        HttpContext.Session.SetString("Success", _localization.GetLocalizedString("Messages.SuccessMessages.SaveGeneralSuccess"));
                        return RedirectToAction("View", new { id = faqUpdateViewModel.FAQ.Id });
                    }
                }
            }
            catch (Exception ex)
            {
                faqUpdateViewModel.FAQTypes = await GetFAQTypes();
                _exceptionLogging.Log(ex);
            }
            return View(faqUpdateViewModel);
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var response = await _faqService.DeleteAsync<APIResponse>(id);
                if (response?.IsSuccess == true && response.StatusCode == HttpStatusCode.NoContent)
                {
                    HttpContext.Session.SetString("Success", _localization.GetLocalizedString("Messages.SuccessMessages.DeleteGeneralSuccess"));
                    return Json(new { success = true });
                }
            }
            catch (Exception ex)
            {
                _exceptionLogging.Log(ex);
            }

            HttpContext.Session.SetString("Error", _localization.GetLocalizedString("Messages.ErrorMessages.GeneralError"));
            return Json(new { success = false });
        }
        #endregion

        #region Utilities
        public async Task<IEnumerable<SelectListItem>> GetFAQTypes()
        {
            IEnumerable<FAQTypeDTO> faqTypes = null;

            var response = await _faqTypeService.GetAllAsync<APIResponse>();
            if (response?.IsSuccess == true && null != response.Result && response.StatusCode == HttpStatusCode.OK)
                faqTypes = JsonConvert.DeserializeObject<IEnumerable<FAQTypeDTO>>(Convert.ToString(response.Result));

            return (faqTypes ?? new List<FAQTypeDTO>()).Select(t => new SelectListItem(string.Concat(t.NameAr, " --- ", t.NameEn), t.Id.ToString())).ToList();
        }

        #endregion
    }
}