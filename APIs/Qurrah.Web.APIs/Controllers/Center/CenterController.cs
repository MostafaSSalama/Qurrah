using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Qurrah.Data.Repository.IRepository;
using Qurrah.Entities;
using Qurrah.Web.APIs.Handlers;
using Qurrah.Web.APIs.Models;
using Qurrah.Web.APIs.Models.DTOs.Center;
using Qurrah.Web.APIs.Utilities;
using System.Net;

namespace Qurrah.Web.APIs.Controllers.Center
{
    [Route("api/CenterAPI")]
    [ApiController]
    public class CenterController : ControllerBase
    {
        #region Fields
        private readonly ICenterHandler _centerHandler;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Ctor
        public CenterController(ICenterHandler centerHandler, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _centerHandler = centerHandler;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region APIs
        [HttpPost("Create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> Create([FromBody] CenterCreateRequest request)
        {
            try
            {
                var center = _mapper.Map<Entities.Center>(request.Center);
                ValidateResult result = await _centerHandler.ValidateCenter(center);
                if (!result.IsValid)
                    return BadRequest(new APIResponse(false, HttpStatusCode.BadRequest, null, new List<string[]> { result.ErrorCodes.ToArray() }));

                var localizedProperties = _mapper.Map<IEnumerable<LocalizedProperty>>(request.LocalizedProperties).ToList();
                await _unitOfWork.Center.AddWithLocalizedPropertiesWithSaveAsync(center, localizedProperties);

                APIResponse apiResponse = new APIResponse(true, HttpStatusCode.Created, null);
                return Ok(apiResponse);
            }
            catch (Exception ex)
            {
                return ExceptionUtility.HandleException(ex);
            }
        }
        #endregion
    }
}