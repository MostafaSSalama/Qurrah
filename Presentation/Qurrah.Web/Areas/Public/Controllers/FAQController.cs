using AutoMapper;
using Localization.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Qurrah.Business.Logging;
using Qurrah.Integration.ServiceWrappers;
using Qurrah.Integration.ServiceWrappers.DTOs.FAQ;
using Qurrah.Integration.ServiceWrappers.Services.IServices;
using System.Net;

namespace Qurrah.Web.Areas.Public.Controllers
{
    [Area("Public")]
    public class FAQController : Controller
    {
        #region Fields
        IFAQService _faqService;
        private LanguageService _localization;
        IExceptionLogging _exceptionLogging;
        IMapper _mapper;
        #endregion

        #region Ctor
        public FAQController(IFAQService faqService, IMapper mapper, LanguageService localization, IExceptionLogging exceptionLogging)
        {
            _faqService = faqService;
            _localization = localization;
            _exceptionLogging = exceptionLogging;
            _mapper = mapper;
        }
        #endregion

        #region Actions
        public async Task<ActionResult> Index()
        {
            IEnumerable<FAQClassifiedDTO> faqsClassified = null;
            try
            {
                var response = await _faqService.GetAllClassifiedByTypeAsync<APIResponse>();
                if (response?.IsSuccess == true && response.StatusCode == HttpStatusCode.OK && null != response.Result)
                    faqsClassified = JsonConvert.DeserializeObject<IEnumerable<FAQClassifiedDTO>>(Convert.ToString(response.Result.ToString())).OrderBy(q => q.Type.DisplayOrder);
            }
            catch (Exception ex)
            {
                HttpContext.Session.SetString(Constants.Session_Error, _localization.GetLocalizedString("Messages.ErrorMessages.GeneralError"));
                _exceptionLogging.Log(ex);
            }
            return View(faqsClassified ?? new List<FAQClassifiedDTO>());
        }
        #endregion
    }
}