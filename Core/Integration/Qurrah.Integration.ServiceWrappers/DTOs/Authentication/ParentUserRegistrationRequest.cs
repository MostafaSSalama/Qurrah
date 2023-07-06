using System.ComponentModel.DataAnnotations;

namespace Qurrah.Integration.ServiceWrappers.DTOs.Authentication
{
    public class ParentUserRegistrationRequest
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Validation.Required.GeneralErrorMessage")]
        [StringLength(150, ErrorMessage = "Validation.StringLength.150")]
        [Display(Name = "Registration.ParentUser.FirstName")]
        public string FirstName { get; set; }

        [StringLength(150, ErrorMessage = "Validation.StringLength.150")]
        [Display(Name = "Registration.ParentUser.SecondName")]
        public string SecondName { get; set; }

        [StringLength(150, ErrorMessage = "Validation.StringLength.150")]
        [Display(Name = "Registration.ParentUser.ThirdName")]
        public string ThirdName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Validation.Required.GeneralErrorMessage")]
        [StringLength(150, ErrorMessage = "Validation.StringLength.150")]
        [Display(Name = "Registration.ParentUser.FourthName")]
        public string FourthName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Validation.Required.GeneralErrorMessage")]
        [RegularExpression(@"0[1-9][0-9]{8}", ErrorMessage = "Validation.RegularExpression.10Digits")]
        [Display(Name = "Registration.ParentUser.MobileNumber")]
        public string MobileNumber { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Validation.Required.GeneralErrorMessage")]
        [Display(Name = "Registration.ParentUser.Gender")]
        public byte FKGenderId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Validation.Required.GeneralErrorMessage")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Validation.RegularExpression.10Digits")]
        [Display(Name = "Registration.ParentUser.IdNumber")]
        public string IdNumber { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Validation.Required.GeneralErrorMessage")]
        [Display(Name = "Registration.ParentUser.UserName")]
        public string UserName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Validation.Required.GeneralErrorMessage")]
        [Display(Name = "Registration.ParentUser.Email")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Validation.Required.GeneralErrorMessage")]
        [RegularExpression(@"0[1-9][0-9]{8}", ErrorMessage = "Validation.RegularExpression.10Digits")]
        [Display(Name = "Registration.ParentUser.PhoneNumber")]
        public string PhoneNumber { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Validation.Required.GeneralErrorMessage")]
        [Display(Name = "Registration.ParentUser.Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Validation.Required.GeneralErrorMessage")]
        [Display(Name = "Registration.ParentUser.ConfirmPassword")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Validation.Compare.NotMatchedPassword")]
        public string ConfirmPassword { get; set; }
    }
}