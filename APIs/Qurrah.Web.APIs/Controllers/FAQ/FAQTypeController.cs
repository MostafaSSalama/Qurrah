using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Qurrah.Data.Repository.IRepository;
using Qurrah.Entities;
using Qurrah.Web.APIs.Models;
using Qurrah.Web.APIs.Models.DTOs.FAQType;
using Qurrah.Web.APIs.Utilities;
using System.Net;

namespace Qurrah.Web.APIs.Controllers.FAQ
{
    [Route("api/FAQTypeAPI")]
    [ApiController]
    public class FAQTypeController : ControllerBase
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        #endregion

        #region Ctor
        public FAQTypeController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        #endregion

        #region APIs
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<APIResponse>> GetAll()
        {
            try
            {
                var faqTypes = await _unitOfWork.FAQType.GetAllAsync();
                var faqTypesDTO = _mapper.Map<IEnumerable<FAQTypeDTO>>(faqTypes);
                return Ok(new APIResponse(true, HttpStatusCode.OK, faqTypesDTO.OrderBy(q => q.DisplayOrder)));
            }
            catch (Exception ex)
            {
                return ExceptionUtility.HandleException(ex);
            }
        }

        [HttpGet("{id:int}", Name = "GetFAQType")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<APIResponse>> Get(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest(new APIResponse(false, HttpStatusCode.BadRequest, null));

                var faqTypeWithLocalizedProps = await _unitOfWork.FAQType.SingleOrDefaultWithLocalizedProperties(f => f.Id == id, tracked: false);
                if (null == faqTypeWithLocalizedProps)
                    return NotFound(new APIResponse(false, HttpStatusCode.NotFound, false));

                var faqTypeDTO = _mapper.Map<FAQTypeWithLocalizedPropertiesDTO>(faqTypeWithLocalizedProps);
                return Ok(new APIResponse(true, HttpStatusCode.OK, faqTypeDTO));
            }
            catch (Exception ex)
            {
                return ExceptionUtility.HandleException(ex);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<APIResponse>> Create([FromBody] FAQTypeCreateRequest faqTypeCreateRequest)
        {
            try
            {
                var faqType = _mapper.Map<FAQType>(faqTypeCreateRequest.FAQType);
                var localizedProperties = _mapper.Map<List<LocalizedProperty>>(faqTypeCreateRequest.LocalizedProperties);

                await _unitOfWork.FAQType.AddWithLocaliedPropertiesWithSaveAsync(faqType, localizedProperties);

                var faqDTO = _mapper.Map<FAQTypeDTO>(faqType);
                APIResponse apiResponse = new APIResponse(true, HttpStatusCode.Created, faqDTO);
                return CreatedAtRoute("GetFAQType", new { id = faqType.Id }, apiResponse);
            }
            catch (Exception ex)
            {
                return ExceptionUtility.HandleException(ex);
            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<APIResponse>> Update([FromBody] FAQTypeUpdateRequest faqTypeUpdateRequest)
        {
            try
            {
                if (faqTypeUpdateRequest.FAQType.Id <= 0 || faqTypeUpdateRequest.LocalizedProperties?.Any(lp => lp.EntityId > 0 && lp.EntityId != faqTypeUpdateRequest.FAQType.Id) == true)
                    return BadRequest(new APIResponse(false, HttpStatusCode.BadRequest, null));

                var faqType = _mapper.Map<FAQType>(faqTypeUpdateRequest.FAQType);
                var localizedProperties = _mapper.Map<List<LocalizedProperty>>(faqTypeUpdateRequest.LocalizedProperties);

                Entities.ActionResult actionResult = await _unitOfWork.FAQType.UpdateWithLocalizedPropertiesWithSaveAsync(faqType, localizedProperties);
                if (actionResult == Entities.ActionResult.ItemNotFound)
                    return NotFound(new APIResponse(false, HttpStatusCode.NotFound, null));

                return Ok(new APIResponse(true, HttpStatusCode.NoContent, null));
            }
            catch (Exception ex)
            {
                return ExceptionUtility.HandleException(ex);
            }
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<APIResponse>> Delete(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest(new APIResponse(false, HttpStatusCode.BadRequest, null));

                Entities.ActionResult actionResult = await _unitOfWork.FAQType.RemoveWithLocalizedPropertiesWithSaveAsync(id);
                if (actionResult == Entities.ActionResult.ItemNotFound)
                    return NotFound(new APIResponse(false, HttpStatusCode.NotFound, null));

                return Ok(new APIResponse(true, HttpStatusCode.NoContent, null));
            }
            catch (Exception ex)
            {
                return ExceptionUtility.HandleException(ex);
            }
        }
        #endregion
    }
}