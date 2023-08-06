using Newtonsoft.Json;
using Qurrah.Business.Extensions;
using Qurrah.Business.Logging;
using Qurrah.Integration.ServiceWrappers;
using FileDTOs = Qurrah.Integration.ServiceWrappers.DTOs.File;
using Qurrah.Integration.ServiceWrappers.Services.IServices;
using System.Net;

namespace Qurrah.Business.File
{
    public class FileManager : IFileManager
    {
        #region Fields
        private readonly IFileService _fileService;
        private readonly IExceptionLogging _exceptionLogging;
        #endregion

        #region Ctor
        public FileManager(IFileService fileService, IExceptionLogging exceptionLogging)
        {
            _fileService = fileService;
            _exceptionLogging = exceptionLogging;
        }
        #endregion

        public async Task<APIResult> UploadSingleFileAsync(FileDTOs.FileInfo file)
        {
            APIResult apiResult = new APIResult();
            try
            {
                var response = await _fileService.UploadSingleFileAsync<APIResponse>(file);

                if (response?.IsSuccess == true && response.StatusCode == HttpStatusCode.Created)
                    apiResult.ActionResult = ActionResult.Success;
                else if (response?.StatusCode == HttpStatusCode.InternalServerError)
                {
                    apiResult.ActionResult = ActionResult.InternalServerError;
                    apiResult.ErrorMessages = response.Errors.ToFlatList();
                }
                else if (response?.StatusCode == HttpStatusCode.BadRequest)
                {
                    apiResult.ActionResult = ActionResult.BadRequest;
                    apiResult.ErrorMessages = response.Errors.ToFlatList();
                }
                else
                    apiResult.ActionResult = ActionResult.GeneralFailure;
            }
            catch (Exception ex)
            {
                apiResult.ActionResult = ActionResult.GeneralFailure;
                throw;
            }
            return apiResult;
        }

        public async Task<APIResult> UploadMultipleFilesAsync(IEnumerable<FileDTOs.FileInfo> files)
        {
            APIResult apiResult = new APIResult();
            try
            {
                var response = await _fileService.UploadMultipleFilesAsync<APIResponse>(files);

                if (response?.IsSuccess == true && response.StatusCode == HttpStatusCode.Created)
                    apiResult.ActionResult = ActionResult.Success;
                else if (response?.StatusCode == HttpStatusCode.InternalServerError)
                {
                    apiResult.ActionResult = ActionResult.InternalServerError;
                    apiResult.ErrorMessages = response.Errors.ToFlatList();
                }
                else if (response?.StatusCode == HttpStatusCode.BadRequest)
                {
                    apiResult.ActionResult = ActionResult.BadRequest;
                    apiResult.ErrorMessages = response.Errors.ToFlatList();
                }
                else
                    apiResult.ActionResult = ActionResult.GeneralFailure;
            }
            catch (Exception ex)
            {
                apiResult.ActionResult = ActionResult.GeneralFailure;
                throw;
            }
            return apiResult;
        }

        public async Task<APIResult> DownloadFileAsync(Guid fileId)
        {
            throw new NotImplementedException();
        }

        public async Task<APIResult> RemoveSingleFileAsync(Guid fileId)
        {
            throw new NotImplementedException();
        }

        public async Task<APIResult> RemoveMultipleFilesAsync(IEnumerable<Guid> fileIds)
        {
            throw new NotImplementedException();
        }
    }
}