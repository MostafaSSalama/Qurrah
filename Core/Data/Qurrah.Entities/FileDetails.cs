using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Qurrah.Entities
{
    public class FileDetails
    {
        [Key]
        public Guid Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string FileName { get; set; }


        [Required(AllowEmptyStrings = false)]
        [StringLength(50)]
        public string FileExtension { get; set; }

        [Required(AllowEmptyStrings =false)]
        public string FileData { get; set; }

        [Required]
        [ForeignKey(nameof(FileType))]
        public FileTypeId FKFileTypeId { get; set; }

        public FileType FileType { get; set; }

        public List<CenterLicense> CenterLicenses { get; set; }
    }
}