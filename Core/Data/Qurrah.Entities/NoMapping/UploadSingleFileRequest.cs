namespace Qurrah.Entities;
public class UploadSingleFileRequest
{
    public string FileName { get; set; }
    public string FileExtension { get; set; }
    public string FileData { get; set; }
    public int FileTypeId { get; set; }
}