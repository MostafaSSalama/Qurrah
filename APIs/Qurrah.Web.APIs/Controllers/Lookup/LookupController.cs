using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Qurrah.Data.Repository.IRepository;
using Qurrah.Web.APIs.Models;
using Qurrah.Web.APIs.Models.DTOs.Lookup;
using Qurrah.Web.APIs.Utilities;
using System.Net;

namespace Qurrah.Web.APIs.Controllers.Lookup
{
    [ApiController]
    [Route("api/LookupAPI")]
    public class LookupController : ControllerBase
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        #endregion

        #region Ctor
        public LookupController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        #endregion

        #region APIs
        [ResponseCache(CacheProfileName = "Default1Day")]
        [HttpGet("GetAllGenders")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> GetAllGenders(string culture)
        {
            try
            {
                var result = await _unitOfWork.GenderDescription.GetAllGenders(culture);
                return Ok(new APIResponse(true, HttpStatusCode.OK, _mapper.Map<IEnumerable<LookupInfoDTO>>(result)));
            }
            catch (Exception ex)
            {
                return ExceptionUtility.HandleException(ex);
            }
        }

        [ResponseCache(CacheProfileName = "Default1Day")]
        [HttpGet("GetAllUserTypes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> GetAllUserTypes(string culture)
        {
            try
            {
                var result = await _unitOfWork.UserTypeDescription.GetAllUserTypes(culture);
                return Ok(new APIResponse(true, HttpStatusCode.OK, _mapper.Map<IEnumerable<LookupInfoDTO>>(result)));
            }
            catch (Exception ex)
            {
                return ExceptionUtility.HandleException(ex);
            }
        }
        #endregion
    }
}
