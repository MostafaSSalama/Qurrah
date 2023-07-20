namespace Qurrah.Web.APIs.Models.DTOs.File
{
    public class FileDTO
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public string FileData { get; set; }
        public int FileTypeId { get; set; }
    }
}