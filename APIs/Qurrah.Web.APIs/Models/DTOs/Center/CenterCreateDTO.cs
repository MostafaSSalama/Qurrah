using Qurrah.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Qurrah.Web.APIs.Models.DTOs.Center
{
    public class CenterCreateDTO
    {
        [Required(AllowEmptyStrings = false)]
        [StringLength(500)]
        public string Name { get; set; }

        [ForeignKey(nameof(CenterType))]
        public CenterTypeId CenterTypeId { get; set; }

        [Required]
        public string CreatedByUserId { get; set; }

        [StringLength(24, MinimumLength = 24)]
        public string IBAN { get; set; }

        [ForeignKey(nameof(File))]
        public Guid? IBANFileId { get; set; }

        [Required]
        public List<CenterLicenseCreateDTO> CenterLicenses { get; set; }
    }
}