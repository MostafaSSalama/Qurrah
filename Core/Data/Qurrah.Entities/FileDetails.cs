using System.ComponentModel.DataAnnotations;

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

        [Required(AllowEmptyStrings = false)]
        public string FileData { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string ContentType { get; set; }

        public List<CenterLicense> CenterLicenses { get; set; }
        public List<Center> Centers { get; set; }
    }
}