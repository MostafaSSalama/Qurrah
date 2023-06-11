using AutoMapper;
using Localization.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Qurrah.Integration.ServiceWrappers.Services.IServices;
using Qurrah.Models.Integration;
using Qurrah.Models.Integration.DTOs.FAQType;
using System.Net;

namespace Qurrah.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FAQTypeController : Controller
    {

        #region Fields
        private readonly IFAQTypeService _faqTypeService;
        private LanguageService _localization;
        private readonly IMapper _mapper;
        #endregion

        #region Ctor
        public FAQTypeController(IFAQTypeService faqTypeService, IMapper mapper, LanguageService localization)
        {
            _faqTypeService = faqTypeService;
            _localization = localization;
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
                //TODO : Add logging
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
                //TODO : Add logging
            }
            return NotFound();
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
                //TODO : Add logging
            }

            HttpContext.Session.SetString("Error", _localization.GetLocalizedString("Messages.ErrorMessages.GeneralError"));
            return Json(new { success = false });
        }

        [HttpGet]
        public async Task<ActionResult> Create()
        {
            return View();
        }

        [HttpPost]
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
                //TODO : Add logging
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
                //TODO : Add logging
            }
            return NotFound();
        }

        [HttpPost(Name = "Update")]
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
                //TODO : Add logging
            }
            return View(faqTypeUpdateDTO);
        }
        #endregion
    }
}