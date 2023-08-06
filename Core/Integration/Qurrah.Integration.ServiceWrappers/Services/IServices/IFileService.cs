using FileDTOs = Qurrah.Integration.ServiceWrappers.DTOs.File;

namespace Qurrah.Integration.ServiceWrappers.Services.IServices
{
    public interface IFileService
    {
        Task<T> UploadSingleFileAsync<T>(FileDTOs.FileInfo file);
        Task<T> UploadMultipleFilesAsync<T>(IEnumerable<FileDTOs.FileInfo> files);
        Task<T> DownloadFileAsync<T>(Guid fileId);
        Task<T> RemoveSingleFileAsync<T>(Guid fileId);
        Task<T> RemoveMultipleFilesAsync<T>(IEnumerable<Guid> fileIds);
    }
}