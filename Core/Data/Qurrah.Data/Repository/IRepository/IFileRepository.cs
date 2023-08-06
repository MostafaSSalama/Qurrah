using Qurrah.Entities;

namespace Qurrah.Data.Repository.IRepository
{
    public interface IFileRepository
    {
        Task UploadSingleFileWithSaveAsync(FileDetails file);
        Task UploadMultipleFilesWithSaveAsync(IEnumerable<FileDetails> files);
        Task<ActionResult> RemoveSingleFileWithSaveAsync(Guid fileId);
        Task<ActionResult> RemoveMultipleFilesWithSaveAsync(IEnumerable<Guid> fileIds);
        Task<DownloadFileResult> DownloadFile(Guid fileId);
    }
}