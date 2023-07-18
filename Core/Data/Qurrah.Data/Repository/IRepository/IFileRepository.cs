using Qurrah.Entities;

namespace Qurrah.Data.Repository.IRepository
{
    public interface IFileRepository
    {
        Task<UploadSingleFileResult> UploadSingleFileWithSaveAsync(UploadSingleFileRequest file);
        Task<UploadMultipleFilesResult> UploadMultipleFilesWithSaveAsync(UploadMultipleFilesRequest files);
        Task<ActionResult> RemoveSingleFileWithSaveAsync(Guid fileId);
        Task<ActionResult> RemoveMultipleFilesWithSaveAsync(IEnumerable<Guid> fileIds);
        Task<DownloadFileResult> DownloadFile(Guid fileId);
    }
}