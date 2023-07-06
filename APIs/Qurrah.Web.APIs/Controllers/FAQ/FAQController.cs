using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Qurrah.Data.Repository.IRepository;
using Qurrah.Entities;
using Qurrah.Web.APIs.Models;
using Qurrah.Web.APIs.Models.DTOs.FAQ;
using Qurrah.Web.APIs.Utilities;
using System.Net;

namespace Qurrah.Web.APIs.Controllers.FAQ
{
    [Route("api/FAQAPI")]
    [ApiController]
    public class FAQController : ControllerBase
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        #endregion

        #region Ctor
        public FAQController(IUnitOfWork unitOfWork, IMapper mapper)
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
                var faqs = await _unitOfWork.FAQ.GetAllAsync(includedProperties: "FAQType");
                var faqsDTO = _mapper.Map<IEnumerable<FAQDTO>>(faqs);
                return Ok(new APIResponse(true, HttpStatusCode.OK, faqsDTO.OrderBy(f => f.DisplayOrder)));
            }
            catch (Exception ex)
            {
                return APIUtility.HandleException(ex);
            }
        }

        [HttpGet("GetAllClassifiedByType")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> GetAllClassifiedByTypeAsync()
        {
            try
            {
                var faqsClassified = await _unitOfWork.FAQ.GetAllClassifiedByTypeAsync();
                var faqsClassifiedDTO = _mapper.Map<IEnumerable<FAQClassifiedWithLocalizedPropertiesDTO>>(faqsClassified);
                return Ok(new APIResponse(true, HttpStatusCode.OK, faqsClassifiedDTO));
            }
            catch (Exception ex)
            {
                return APIUtility.HandleException(ex);
            }
        }

        [HttpGet("{id:int}", Name = "GetFAQ")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<APIResponse>> Get(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest(new APIResponse(false, HttpStatusCode.BadRequest, null));

                var faqWithLocalizedProps = await _unitOfWork.FAQ.SingleOrDefaultWithLocalizedProperties(f => f.Id == id, "FAQType", tracked: false);
                if (null == faqWithLocalizedProps)
                    return NotFound(new APIResponse(false, HttpStatusCode.NotFound, false));

                var faqDTO = _mapper.Map<FAQWithLocalizedPropertiesDTO>(faqWithLocalizedProps);
                return Ok(new APIResponse(true, HttpStatusCode.OK, faqDTO));
            }
            catch (Exception ex)
            {
                return APIUtility.HandleException(ex);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<APIResponse>> Create([FromBody] FAQCreateRequest faqCreateRequest)
        {
            try
            {
                var faq = _mapper.Map<Entities.FAQ>(faqCreateRequest.FAQ);
                var localizedProperties = _mapper.Map<List<LocalizedProperty>>(faqCreateRequest.LocalizedProperties);

                await _unitOfWork.FAQ.AddWithLocaliedPropertiesWithSaveAsync(faq, localizedProperties);

                var faqDTO = _mapper.Map<FAQDTO>(faq);
                APIResponse apiResponse = new APIResponse(true, HttpStatusCode.Created, faqDTO);
                return CreatedAtRoute("GetFAQ", new { id = faq.Id }, apiResponse);
            }
            catch (Exception ex)
            {
                return APIUtility.HandleException(ex);
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
        public async Task<ActionResult<APIResponse>> Update([FromBody] FAQUpdateRequest faqUpdateRequest)
        {
            try
            {
                if (faqUpdateRequest.FAQ.Id <= 0 || faqUpdateRequest.LocalizedProperties?.Any(lp => lp.EntityId > 0 && lp.EntityId != faqUpdateRequest.FAQ.Id) == true)
                    return BadRequest(new APIResponse(false, HttpStatusCode.BadRequest, null));

                var faq = _mapper.Map<Entities.FAQ>(faqUpdateRequest.FAQ);
                var localizedProperties = _mapper.Map<List<LocalizedProperty>>(faqUpdateRequest.LocalizedProperties);

                Entities.ActionResult actionResult = await _unitOfWork.FAQ.UpdateWithLocalizedPropertiesWithSaveAsync(faq, localizedProperties);
                if (actionResult == Entities.ActionResult.ItemNotFound)
                    return NotFound(new APIResponse(false, HttpStatusCode.NotFound, null));

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

                Entities.ActionResult actionResult = await _unitOfWork.FAQ.RemoveWithLocalizedPropertiesWithSaveAsync(id);
                if (actionResult == Entities.ActionResult.ItemNotFound)
                    return NotFound(new APIResponse(false, HttpStatusCode.NotFound, null));

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