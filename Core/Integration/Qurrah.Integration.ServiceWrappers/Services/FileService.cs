using Microsoft.Extensions.Configuration;
using FileDTOs = Qurrah.Integration.ServiceWrappers.DTOs.File;
using Qurrah.Integration.ServiceWrappers.Services.IServices;
using static Qurrah.Integration.ServiceWrappers.Constants;

namespace Qurrah.Integration.ServiceWrappers.Services
{
    public class FileService : BaseService, IFileService
    {
        #region Fields
        private readonly string serviceURL;
        #endregion

        #region Ctor
        public FileService(IHttpClientFactory httpClientFactory, IConfiguration configuration) : base(httpClientFactory)
        {
            serviceURL = configuration.GetValue<string>("ServiceURLs:FileAPI");
        }
        #endregion

        #region Methods
        public async Task<T> UploadSingleFileAsync<T>(FileDTOs.FileInfo file)
        {
            return await SendAsync<T>(new APIRequest
            {
                APIType = APIType.HTTPPost,
                Data = file,
                URL = $"{serviceURL}/UploadSingleFile"
            });
        }

        public async Task<T> UploadMultipleFilesAsync<T>(IEnumerable<FileDTOs.FileInfo> files)
        {
            return await SendAsync<T>(new APIRequest
            {
                APIType = APIType.HTTPPost,
                Data = files,
                URL = $"{serviceURL}/UploadMultipleFiles"
            });
        }

        public async Task<T> DownloadFileAsync<T>(Guid fileId)
        {
            return await SendAsync<T>(new APIRequest
            {
                APIType = APIType.HTTPPost,
                Data = fileId,
                URL = $"{serviceURL}/DownloadFile"
            });
        }

        public async Task<T> RemoveSingleFileAsync<T>(Guid fileId)
        {
            return await SendAsync<T>(new APIRequest
            {
                APIType = APIType.HTTPPost,
                Data = fileId,
                URL = $"{serviceURL}/RemoveSingleFile"
            });
        }

        public async Task<T> RemoveMultipleFilesAsync<T>(IEnumerable<Guid> fileIds)
        {
            return await SendAsync<T>(new APIRequest
            {
                APIType = APIType.HTTPPost,
                Data = fileIds,
                URL = $"{serviceURL}/RemoveMultipleFiles"
            });
        }
        #endregion
    }
}