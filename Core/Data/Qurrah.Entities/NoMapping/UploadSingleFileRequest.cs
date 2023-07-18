namespace Qurrah.Entities;
public class UploadSingleFileRequest
{
    public string FileName { get; set; }
    public string FileData { get; set; }
    public FileTypeId FileType { get; set; }
}