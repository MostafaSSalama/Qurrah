namespace Qurrah.Entities;
public class UploadMultipleFilesRequest
{
    public IEnumerable<UploadSingleFileRequest> Files { get; set; }
}