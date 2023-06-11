using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Qurrah.Data.Repository.IRepository;
using Qurrah.Entities;
using Qurrah.Web.APIs.Models;
using Qurrah.Web.APIs.Models.DTOs.FAQType;
using Qurrah.Web.APIs.Utilities;
using System.Net;

namespace Qurrah.Web.APIs.Controllers
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
                return APIUtility.HandleException(ex);
            }
        }

        [HttpGet("{id:int}", Name = "GetFAQType")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> Get(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest(new APIResponse(false, HttpStatusCode.BadRequest, null));

                var faqType = await _unitOfWork.FAQType.SingleOrDefaultAsync(f => f.Id == id, tracked: false);
                if (null == faqType)
                    return NotFound(new APIResponse(false, HttpStatusCode.NotFound, false));

                var faqTypeDTO = _mapper.Map<FAQTypeDTO>(faqType);
                return Ok(new APIResponse(true, HttpStatusCode.OK, faqTypeDTO));
            }
            catch (Exception ex)
            {
                return APIUtility.HandleException(ex);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<APIResponse>> Create([FromBody] FAQTypeCreateDTO faqTypeCreateDTO)
        {
            try
            {
                var faqType = _mapper.Map<FAQType>(faqTypeCreateDTO);
                await _unitOfWork.FAQType.AddAsync(faqType);
                await _unitOfWork.SaveAsync();

                var faqDTO = _mapper.Map<FAQTypeDTO>(faqType);
                APIResponse apiResponse = new APIResponse(true, HttpStatusCode.Created, faqDTO);
                return CreatedAtRoute("GetFAQType", new { id = faqType.Id }, apiResponse);
            }
            catch (Exception ex)
            {
                return APIUtility.HandleException(ex);
            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> Update([FromBody] FAQTypeUpdateDTO faqTypeUpdateDTO)
        {
            try
            {
                if (faqTypeUpdateDTO.Id <= 0)
                    return BadRequest(new APIResponse(false, HttpStatusCode.BadRequest, null));

                var faqType = await _unitOfWork.FAQType.SingleOrDefaultAsync(f => f.Id == faqTypeUpdateDTO.Id, tracked: false);
                if (null == faqType)
                    return NotFound(new APIResponse(false, HttpStatusCode.NotFound, null));

                faqType = _mapper.Map<FAQType>(faqTypeUpdateDTO);
                _unitOfWork.FAQType.Update(faqType);
                await _unitOfWork.SaveAsync();

                return Ok(new APIResponse(true, HttpStatusCode.NoContent, null));
            }
            catch (Exception ex)
            {
                return APIUtility.HandleException(ex);
            }
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> Delete(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest(new APIResponse(false, HttpStatusCode.BadRequest, null));

                var faqType = await _unitOfWork.FAQType.SingleOrDefaultAsync(f => f.Id == id);
                if (null == faqType)
                    return NotFound(new APIResponse(false, HttpStatusCode.NotFound, null));

                _unitOfWork.FAQType.Remove(faqType);
                await _unitOfWork.SaveAsync();

                return Ok(new APIResponse(true, HttpStatusCode.NoContent, null));
            }
            catch (Exception ex)
            {
                return APIUtility.HandleException(ex);
            }
        }
        #endregion
    }
}