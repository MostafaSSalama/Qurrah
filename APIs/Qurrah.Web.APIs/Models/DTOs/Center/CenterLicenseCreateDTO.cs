using Qurrah.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Qurrah.Web.APIs.Models.DTOs.Center
{
    public class CenterLicenseCreateDTO
    {
        [Required]
        [StringLength(50)]
        public string LicenseNumber { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime ExpiryDate { get; set; }

        [Required]
        public Guid FileId { get; set; }
    }
}