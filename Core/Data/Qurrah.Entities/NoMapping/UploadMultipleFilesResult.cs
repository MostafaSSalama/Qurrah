namespace Qurrah.Entities;
public class UploadMultipleFilesResult
{
    public UploadMultipleFilesResult(bool succeeded, IEnumerable<Guid> fileIds)
    {
        Succeeded = succeeded;
        FileIds = fileIds;
    }
    public bool Succeeded { get; set; }
    public IEnumerable<Guid> FileIds { get; set; }
}