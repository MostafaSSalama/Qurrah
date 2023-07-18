namespace Qurrah.Entities;
public class UploadSingleFileResult
{
    public UploadSingleFileResult(bool succeeded, Guid fileId)
    {
        Succeeded = succeeded;
        FileId = fileId;
    }
    public bool Succeeded { get; set; }
    public Guid FileId { get; set; }
}