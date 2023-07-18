using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Qurrah.Entities
{
    public class CenterLicenseStatusDescription
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(CenterLicenseStatus))]
        public CenterLicenseStatusId FKStatusId { get; set; }
        public CenterLicenseStatus CenterLicenseStatus { get; set; }

        [Required]
        [ForeignKey(nameof(Language))]
        public LanguageId FKLanguageId { get; set; }
        public Language Language { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Description { get; set; }
    }
}