using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Qurrah.Data.Repository.IRepository;
using Qurrah.Entities;
using Qurrah.Web.APIs.Models;
using Qurrah.Web.APIs.Models.DTOs.FAQ;
using Qurrah.Web.APIs.Utilities;
using System.Net;

namespace Qurrah.Web.APIs.Controllers
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
        public async Task<ActionResult<APIResponse>> GetAllClassifiedByTypeAsync()
        {
            try
            {
                var faqsClassified = await _unitOfWork.FAQ.GetAllClassifiedByTypeAsync();
                var faqsClassifiedDTO = _mapper.Map<IEnumerable<FAQClassifiedDTO>>(faqsClassified);
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> Get(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest(new APIResponse(false, HttpStatusCode.BadRequest, null));

                var faq = await _unitOfWork.FAQ.SingleOrDefaultAsync(f => f.Id == id, includedProperties: "FAQType", tracked: false);
                if (null == faq)
                    return NotFound(new APIResponse(false, HttpStatusCode.NotFound, false));

                var faqDTO = _mapper.Map<FAQDTO>(faq);
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
        public async Task<ActionResult<APIResponse>> Create([FromBody] FAQCreateDTO faqCreateDTO)
        {
            try
            {
                var faq = _mapper.Map<FAQ>(faqCreateDTO);
                await _unitOfWork.FAQ.AddAsync(faq);
                await _unitOfWork.SaveAsync();

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
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> Update([FromBody] FAQUpdateDTO faqUpdateDTO)
        {
            try
            {
                if (faqUpdateDTO.Id <= 0)
                    return BadRequest(new APIResponse(false, HttpStatusCode.BadRequest, null));

                var faq = await _unitOfWork.FAQ.SingleOrDefaultAsync(f => f.Id == faqUpdateDTO.Id, tracked: false);
                if (null == faq)
                    return NotFound(new APIResponse(false, HttpStatusCode.NotFound, null));

                faq = _mapper.Map<FAQ>(faqUpdateDTO);
                _unitOfWork.FAQ.Update(faq);
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

                var faq = await _unitOfWork.FAQ.SingleOrDefaultAsync(f => f.Id == id);
                if (null == faq)
                    return NotFound(new APIResponse(false, HttpStatusCode.NotFound, null));

                _unitOfWork.FAQ.Remove(faq);
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