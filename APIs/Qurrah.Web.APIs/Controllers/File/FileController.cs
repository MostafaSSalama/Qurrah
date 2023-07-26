using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Qurrah.Data.Repository.IRepository;
using Qurrah.Entities;
using Qurrah.Web.APIs.Handlers;
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
        private readonly IFileHandler _fileHandler;
        #endregion

        #region Ctor
        public FileController(IUnitOfWork unitOfWork, IMapper mapper, IFileHandler fileHandler)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _fileHandler = fileHandler;
        }
        #endregion

        #region APIs
        [HttpPost("UploadSingleFile")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UploadSingleFile([FromBody] UploadFileDTO file)
        {
            try
            {
                ValidateResult result = _fileHandler.ValidateFile(file);
                if (!result.IsValid)
                    return BadRequest(new APIResponse(false, HttpStatusCode.BadRequest, null, new List<string[]> { result.ErrorCodes.ToArray() }));

                var uploadFileRequest = _mapper.Map<UploadSingleFileRequest>(file);
                var uploadFileResult = await _unitOfWork.File.UploadSingleFileWithSaveAsync(uploadFileRequest);

                var fileDTO = _mapper.Map<FileDTO>(file);
                fileDTO.Id = uploadFileResult.FileId;

                APIResponse apiResponse = new APIResponse(true, HttpStatusCode.Created, fileDTO);
                return CreatedAtRoute("DownloadFile", new { fileId = fileDTO.Id }, apiResponse);
            }
            catch (Exception ex)
            {
                return ExceptionUtility.HandleException(ex);
            }
        }

        [HttpPost("UploadMultipleFile")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UploadMultipleFile([FromBody] UploadMultipleFilesDTO files)
        {
            try
            {
                ValidateFilesResult result = _fileHandler.ValidateFiles(files);

                if (!result.IsValid)
                    return BadRequest(new APIResponse(false
                                                          , HttpStatusCode.BadRequest
                                                          , null
                                                          , result.FileResults.Select(fr => fr.ErrorCodes.ToArray()).ToList()));

                var uploadFilesRequest = _mapper.Map<UploadMultipleFilesRequest>(files);
                var uploadFilesResult = await _unitOfWork.File.UploadMultipleFilesWithSaveAsync(uploadFilesRequest);

                if (!uploadFilesResult.Files.IsNullOrEmpty())
                    throw new Exception("An error occured while uploading the files");

                var filesDTO = _mapper.Map<IEnumerable<FileDTO>>(uploadFilesResult.Files);

                APIResponse apiResponse = new APIResponse(true, HttpStatusCode.Created, filesDTO.Select(f => f.Id));
                return Ok(apiResponse);
            }
            catch (Exception ex)
            {
                return ExceptionUtility.HandleException(ex);
            }
        }

        [HttpGet("{fileId:guid}", Name = "DownloadFile")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> DownloadFile(Guid fileId)
        {
            try
            {
                if (fileId == Guid.Empty)
                    return BadRequest(new APIResponse(false, HttpStatusCode.BadRequest, null));

                var downloadResult = await _unitOfWork.File.DownloadFile(fileId);
                if (null == downloadResult || null == downloadResult.File || downloadResult.ActionResult == Entities.ActionResult.ItemNotFound)
                    return NotFound(new APIResponse(false, HttpStatusCode.NotFound, false));

                var fileDTO = _mapper.Map<FileDTO>(downloadResult.File);
                return Ok(new APIResponse(true, HttpStatusCode.OK, fileDTO));
            }
            catch (Exception ex)
            {
                return ExceptionUtility.HandleException(ex);
            }
        }

        [HttpDelete("RemoveSingleFile")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> RemoveSingleFile(Guid fileId)
        {
            try
            {
                if (fileId == Guid.Empty)
                    return BadRequest(new APIResponse(false, HttpStatusCode.BadRequest, null));

                var removeResult = await _unitOfWork.File.RemoveSingleFileWithSaveAsync(fileId);
                if (removeResult == Entities.ActionResult.ItemNotFound)
                    return NotFound(new APIResponse(false, HttpStatusCode.NotFound, null));

                return Ok(new APIResponse(true, HttpStatusCode.NoContent, null));
            }
            catch (Exception ex)
            {
                return ExceptionUtility.HandleException(ex);
            }
        }

        [HttpDelete("RemoveMultipleFiles")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> RemoveMultipleFiles(IEnumerable<Guid> fileIds)
        {
            try
            {
                if (fileIds.IsNullOrEmpty())
                    return BadRequest(new APIResponse(false
                                                        , HttpStatusCode.BadRequest
                                                        , null
                                                        , new List<string[]>
                                                        {
                                                            new string[]
                                                            {
                                                                Constants.File.EmptyFilesList
                                                            }
                                                        }));
                else if (fileIds.Any(id => id == Guid.Empty))
                    return BadRequest(new APIResponse(false, HttpStatusCode.BadRequest, null));

                var removeResult = await _unitOfWork.File.RemoveMultipleFilesWithSaveAsync(fileIds.Distinct());
                if (removeResult == Entities.ActionResult.ItemNotFound)
                    return NotFound(new APIResponse(false
                                                        , HttpStatusCode.NotFound
                                                        , null
                                                        , new List<string[]>
                                                        {
                                                            new string[]
                                                            {
                                                                Constants.File.AllOrSomeItemsNotFound
                                                            }
                                                        }));

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