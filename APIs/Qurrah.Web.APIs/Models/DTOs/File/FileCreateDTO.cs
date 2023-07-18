namespace Qurrah.Web.APIs.Models.DTOs.File
{
    public class FileCreateDTO
    {
        public IFormFile File { get; set; }
        public string FileName { get; set; }
        public string FileData { get; set; }
        public int FileTypeId { get; set; }
    }
}