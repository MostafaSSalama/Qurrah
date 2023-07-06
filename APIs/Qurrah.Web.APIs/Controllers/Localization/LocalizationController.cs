using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Qurrah.Data.Repository.IRepository;
using Qurrah.Entities;
using Qurrah.Web.APIs.Models;
using Qurrah.Web.APIs.Models.DTOs.Localization;
using Qurrah.Web.APIs.Utilities;
using System.Net;

namespace Qurrah.Web.APIs.Controllers.Localization
{
    [ApiController]
    [Route("api/LocalizationAPI")]
    public class LocalizationController : ControllerBase
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        #endregion

        #region Ctor
        public LocalizationController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        #endregion

        #region APIs
        [HttpGet("GetLocales")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetLocales([FromQuery] string culture)
        {
            try
            {
                var result = await _unitOfWork.LanguageDescription.GetLocales(culture);
                return Ok(new APIResponse(true, HttpStatusCode.OK, _mapper.Map<IEnumerable<LocaleDTO>>(result)));
            }
            catch (Exception ex)
            {
                return APIUtility.HandleException(ex);
            }
        }
        #endregion
    }
}
