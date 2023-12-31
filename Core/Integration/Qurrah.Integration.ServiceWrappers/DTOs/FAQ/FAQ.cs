﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Qurrah.Integration.ServiceWrappers.DTOs.FAQ
{
    public class FAQ
    {
        [Required]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Validation.Required.GeneralErrorMessage")]
        [StringLength(1000, ErrorMessage = "Validation.StringLength.GeneralErrorMessage")]
        [Display(Name = "FAQ.Create.Question")]
        [Localized(DisplayValue = "FAQ.Create.Question", LocaleKeyGroup = "FAQ", LocaleKey = "Question", IsMultiLine = false)]
        public string Question { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Validation.Required.GeneralErrorMessage")]
        [Display(Name = "FAQ.Create.Answer")]
        [Localized(DisplayValue = "FAQ.Create.Answer", LocaleKeyGroup = "FAQ", LocaleKey = "Answer", IsMultiLine = true)]
        public string Answer { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Validation.Required.GeneralErrorMessage")]
        [Display(Name = "FAQ.Create.FKTypeId")]
        public int FKTypeId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Validation.Required.GeneralErrorMessage")]
        [Range(1, 1000, ErrorMessage = "Validation.Range.OneToThousandErrorMessage")]
        [Display(Name = "FAQ.Create.DisplayOrder")]
        public int DisplayOrder { get; set; }

        [ValidateNever]
        public FAQType FAQType { get; set; }
    }
}