using System.ComponentModel.DataAnnotations;

namespace Qurrah.Integration.ServiceWrappers.DTOs.Center
{
    public class CenterLicense
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Validation.Required.GeneralErrorMessage")]
        [StringLength(50, ErrorMessage = "Validation.StringLength.GeneralErrorMessage")]
        [Display(Name = "Center.License.Create.LicenseNumber")]
        public string LicenseNumber { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Validation.Required.GeneralErrorMessage")]
        [Display(Name = "Center.License.Create.StartDate")]
        public DateTime? StartDate { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Validation.Required.GeneralErrorMessage")]
        [Display(Name = "Center.License.Create.ExpiryDate")]
        public DateTime? ExpiryDate { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Validation.Required.Center.FKFileId")]
        [Display(Name = "Center.License.Create.FKFileId")]
        public Guid FileId { get; set; }
    }
}
