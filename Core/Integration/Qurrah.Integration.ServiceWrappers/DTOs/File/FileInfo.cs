using System.ComponentModel.DataAnnotations;

namespace Qurrah.Integration.ServiceWrappers.DTOs.File
{
    public class FileInfo
    {
        [Required]
        public Guid Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string FileName { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(50)]
        public string FileExtension { get; set; }
        
        [Required(AllowEmptyStrings = false)]
        public string FileData { get; set; }
        
        [Required]
        public string ContentType { get; set; }
    }
}