namespace Qurrah.Entities;
public class UploadMultipleFilesResult
{
    public UploadMultipleFilesResult(bool succeeded, IEnumerable<FileDetails> fileIds)
    {
        Succeeded = succeeded;
        Files = fileIds;
    }
    public bool Succeeded { get; set; }
    public IEnumerable<FileDetails> Files { get; set; }
}