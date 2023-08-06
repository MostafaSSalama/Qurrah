using Qurrah.Entities;
using System.ComponentModel.DataAnnotations;

namespace Qurrah.Integration.ServiceWrappers.DTOs.Center
{
    public class Center
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Validation.Required.GeneralErrorMessage")]
        [StringLength(500, ErrorMessage = "Validation.StringLength.GeneralErrorMessage")]
        [Display(Name = "Center.Create.Name")]
        [Localized(DisplayValue = "Center.Create.Name", LocaleKeyGroup = "Center", LocaleKey = "Name", IsMultiLine = false)]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Validation.Required.GeneralErrorMessage")]
        [StringLength(24, MinimumLength = 24, ErrorMessage = "Validation.StringLength.GeneralErrorMessage")]
        [Display(Name = "Center.Create.IBAN")]
        [RegularExpression("^SA([0-9]){22}$", ErrorMessage = "Validation.RegularExp.InvalidIBAN")]
        public string IBAN { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Validation.Required.GeneralErrorMessage")]
        [StringLength(24, MinimumLength = 24)]
        [Display(Name = "Center.Create.ConfirmIBAN")]
        [Compare(nameof(IBAN), ErrorMessage = "Validation.Compare.NotMatchedIBAN")]
        [RegularExpression("^SA([0-9]){22}$", ErrorMessage = "Validation.RegularExp.InvalidIBAN")]
        public string ConfirmIBAN { get; set; }

        [Required(ErrorMessage = "Validation.Required.GeneralErrorMessage")]
        [Display(Name = "Center.Create.FKCenterTypeId")]
        public CenterTypeId CenterTypeId { get; set; }

        [Required(ErrorMessage = "Validation.Required.Center.IBANFile")]
        [Display(Name = "Center.Create.FKIBANFileId")]
        public Guid IBANFileId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Validation.Required.GeneralErrorMessage")]
        public string CreatedByUserId { get; set; }

        [Required(ErrorMessage = "Validation.Required.GeneralErrorMessage")]
        public List<CenterLicense> CenterLicenses { get; set; }
    }
}