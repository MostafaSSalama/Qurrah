namespace Qurrah.Web.APIs.Models.DTOs.File
{
    public class UploadMultipleFilesDTO
    {
        public IEnumerable<UploadFileDTO> Files { get; set; }
    }
}