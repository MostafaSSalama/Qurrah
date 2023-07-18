using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Qurrah.Data.Repository.IRepository;
using Qurrah.Web.APIs.Models;
using Qurrah.Web.APIs.Models.DTOs.File;
using Qurrah.Web.APIs.Utilities;
using System.Net;

namespace Qurrah.Web.APIs.Controllers.File
{
    [Route("api/FileAPI")]
    [ApiController]
    public class FileController : ControllerBase
    {
        #region Fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        #endregion


        #region Ctor
        public FileController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        #endregion

        #region APIs
        [HttpPost]
        public async Task<ActionResult<APIResponse>> UploadSingleFile([FromForm] FileCreateDTO fileCreateDTO)
        {
            try
            {
                return Ok(fileCreateDTO);
            }
            catch (Exception ex)
            {
                return APIUtility.HandleException(ex);
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> DownloadFile(Guid fileId)
        {
            try
            {
                if(fileId == Guid.Empty)
                    return BadRequest(new APIResponse(false, HttpStatusCode.BadRequest, null));

                var downloadResult = await _unitOfWork.File.DownloadFile(fileId);
                if (null == downloadResult || null == downloadResult.File || downloadResult.ActionResult == Entities.ActionResult.ItemNotFound)
                    return NotFound(new APIResponse(false, HttpStatusCode.NotFound, false));

                var fileDTO = _mapper.Map<FileDTO>(downloadResult);
                return Ok(new APIResponse(true, HttpStatusCode.OK, fileDTO));
            }
            catch (Exception ex)
            {
                return APIUtility.HandleException(ex);
            }
        }
        #endregion
    }
}