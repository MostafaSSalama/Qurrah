using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Qurrah.Entities
{
    public class CenterLicenseStatus
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public CenterLicenseStatusId Id { get; set; }

        [Required]
        [StringLength(500)]
        public string Name { get; set; }

        public List<CenterLicenseStatusDescription> CenterLicenseStatusDescriptions { get; set; }
    }
}