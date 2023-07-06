using System.ComponentModel.DataAnnotations;

namespace Qurrah.Integration.ServiceWrappers.DTOs.FAQ
{
    public class FAQType
    {
        [Required]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Validation.Required.GeneralErrorMessage")]
        [StringLength(1000, ErrorMessage = "Validation.StringLength.GeneralErrorMessage")]
        [Display(Name = "FAQType.Create.Name")]
        public string Name { get; set; }

        [Display(Name = "FAQType.Create.Description")]
        public string Description { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Validation.Required.GeneralErrorMessage")]
        [Range(1, 1000, ErrorMessage = "Validation.Range.OneToThousandErrorMessage")]
        [Display(Name = "FAQType.Create.DisplayOrder")]
        public int DisplayOrder { get; set; }
    }
}