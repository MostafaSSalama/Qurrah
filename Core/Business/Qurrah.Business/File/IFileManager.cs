using FileDTOs = Qurrah.Integration.ServiceWrappers.DTOs.File;

namespace Qurrah.Business.File
{
    public interface IFileManager
    {
        Task<APIResult> UploadSingleFileAsync(FileDTOs.FileInfo file);
        Task<APIResult> UploadMultipleFilesAsync(IEnumerable<FileDTOs.FileInfo> files);
        Task<APIResult> DownloadFileAsync(Guid fileId);
        Task<APIResult> RemoveSingleFileAsync(Guid fileId);
        Task<APIResult> RemoveMultipleFilesAsync(IEnumerable<Guid> fileIds);
    }
}