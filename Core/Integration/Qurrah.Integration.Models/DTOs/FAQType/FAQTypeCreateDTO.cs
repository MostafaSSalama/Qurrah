using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Qurrah.Integration.Models.DTOs.FAQType
{
    public class FAQTypeCreateDTO
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Validation.Required.GeneralErrorMessage")]
        [StringLength(10, ErrorMessage = "Validation.StringLength.GeneralErrorMessage")]
        [Display(Name = "FAQType.Create.NameAr")]
        public string NameAr { get; set; }

        [Display(Name = "FAQType.Create.DescriptionAr")]
        public string DescriptionAr { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Validation.Required.GeneralErrorMessage")]
        [StringLength(10, ErrorMessage = "Validation.StringLength.GeneralErrorMessage")]
        [Display(Name = "FAQType.Create.NameEn")]
        public string NameEn { get; set; }

        [Display(Name = "FAQType.Create.DescriptionEn")]
        public string DescriptionEn { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Validation.Required.GeneralErrorMessage")]
        [Range(1, 1000, ErrorMessage = "Validation.Range.OneToThousandErrorMessage")]
        [Display(Name = "FAQType.Create.DisplayOrder")]
        public int DisplayOrder { get; set; }
    }
}