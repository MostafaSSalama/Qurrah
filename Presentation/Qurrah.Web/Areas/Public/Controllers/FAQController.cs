using AutoMapper;
using Qurrah.Business.Localization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Qurrah.Business.Logging;
using Qurrah.Integration.ServiceWrappers;
using Qurrah.Integration.ServiceWrappers.Services.IServices;
using System.Net;
using Qurrah.Integration.ServiceWrappers.DTOs.FAQ;
using Qurrah.Business.FAQ;
using Qurrah.Business.Extensions;

namespace Qurrah.Web.Areas.Public.Controllers
{
    [Area("Public")]
    public class FAQController : Controller
    {
        #region Fields
        private readonly IFAQManager _faqManager;
        private readonly LanguageService _localization;
        private readonly IExceptionLogging _exceptionLogging;
        private readonly IMapper _mapper;
        #endregion

        #region Ctor
        public FAQController(IFAQManager faqManager, IMapper mapper, LanguageService localization, IExceptionLogging exceptionLogging)
        {
            _faqManager = faqManager;
            _localization = localization;
            _exceptionLogging = exceptionLogging;
            _mapper = mapper;
        }
        #endregion

        #region Actions
        public async Task<ActionResult> Index()
        {
            IEnumerable<FAQClassified> faqsClassified = null;
            try
            {
                var result = await _faqManager.GetAllClassifiedByTypeAsync(Thread.CurrentThread.CurrentCulture.Name);
                if (result.ActionResult == Business.ActionResult.Success)
                    faqsClassified = result.Result as List<FAQClassified>;
                else
                {
                    HttpContext.Session.SetString(Business.Constants.Session_Error, _localization.GetLocalizedString("Messages.ErrorMessages.GeneralError"));

                    if (result?.ActionResult == Business.ActionResult.InternalServerError && result.ErrorMessages?.Any() == true)
                        _exceptionLogging.Log(result.ErrorMessages.Concatenate());
                    else
                        _exceptionLogging.Log("An error occured while loading classified FAQs!");
                }
            }
            catch (Exception ex)
            {
                HttpContext.Session.SetString(Business.Constants.Session_Error, _localization.GetLocalizedString("Messages.ErrorMessages.GeneralError"));
                _exceptionLogging.Log(ex);
            }

            return View(faqsClassified ?? new List<FAQClassified>());
        }
        #endregion
    }
}