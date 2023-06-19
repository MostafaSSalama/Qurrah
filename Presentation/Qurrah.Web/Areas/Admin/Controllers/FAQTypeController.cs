using AutoMapper;
using Localization.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Qurrah.Business.Logging;
using Qurrah.Integration.ServiceWrappers;
using Qurrah.Integration.ServiceWrappers.DTOs.FAQType;
using Qurrah.Integration.ServiceWrappers.Services.IServices;
using System.Net;

namespace Qurrah.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Administrator")]
    public class FAQTypeController : Controller
    {
        #region Fields
        private readonly IFAQTypeService _faqTypeService;
        private LanguageService _localization;
        IExceptionLogging _exceptionLogging;
        private readonly IMapper _mapper;
        #endregion

        #region Ctor
        public FAQTypeController(IFAQTypeService faqTypeService, IMapper mapper, LanguageService localization, IExceptionLogging exceptionLogging)
        {
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
            IEnumerable<FAQTypeDTO> faqTypes = null;
            try
            {
                var response = await _faqTypeService.GetAllAsync<APIResponse>();
                if (response?.IsSuccess == true && null != response.Result && response.StatusCode == HttpStatusCode.OK)
                    faqTypes = JsonConvert.DeserializeObject<IEnumerable<FAQTypeDTO>>(Convert.ToString(response.Result));
            }
            catch (Exception ex)
            {
                HttpContext.Session.SetString("Error", _localization.GetLocalizedString("Messages.ErrorMessages.GeneralError"));
                _exceptionLogging.Log(ex);
            }
            return View(faqTypes ?? new List<FAQTypeDTO>());
        }

        [HttpGet]
        public async Task<ActionResult> View(int id)
        {
            try
            {
                var response = await _faqTypeService.GetAsync<APIResponse>(id);
                if (response?.IsSuccess == true && response.StatusCode == HttpStatusCode.OK && null != response.Result)
                {
                    var faqType = JsonConvert.DeserializeObject<FAQTypeDTO>(Convert.ToString(response.Result));
                    return View(faqType);
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
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(FAQTypeCreateDTO faqTypeCreateDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _faqTypeService.CreateAsync<APIResponse>(faqTypeCreateDTO);
                    if (response?.IsSuccess == true && response.StatusCode == HttpStatusCode.Created)
                    {
                        var faqType = JsonConvert.DeserializeObject<FAQTypeDTO>(Convert.ToString(response.Result));

                        HttpContext.Session.SetString("Success", _localization.GetLocalizedString("Messages.SuccessMessages.SaveGeneralSuccess"));
                        return RedirectToAction("Index", new { id = faqType.Id });
                    }
                }
            }
            catch (Exception ex)
            {
                _exceptionLogging.Log(ex);
            }
            return View(faqTypeCreateDTO);
        }

        [HttpGet]
        public async Task<ActionResult> Update(int id)
        {
            try
            {
                var response = await _faqTypeService.GetAsync<APIResponse>(id);
                if (response?.IsSuccess == true && response.StatusCode == HttpStatusCode.OK && null != response.Result)
                {
                    var faqType = JsonConvert.DeserializeObject<FAQTypeUpdateDTO>(Convert.ToString(response.Result));
                    return View(faqType);
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
        public async Task<ActionResult> Update(FAQTypeUpdateDTO faqTypeUpdateDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var response = await _faqTypeService.UpdateAsync<APIResponse>(faqTypeUpdateDTO);
                    if (response?.IsSuccess == true && response.StatusCode == HttpStatusCode.NoContent)
                    {
                        HttpContext.Session.SetString("Success", _localization.GetLocalizedString("Messages.SuccessMessages.SaveGeneralSuccess"));
                        return RedirectToAction("View", new { id = faqTypeUpdateDTO.Id });
                    }
                }
            }
            catch (Exception ex)
            {
                _exceptionLogging.Log(ex);
            }
            return View(faqTypeUpdateDTO);
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var response = await _faqTypeService.DeleteAsync<APIResponse>(id);
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
    }
}