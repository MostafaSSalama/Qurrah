using System.ComponentModel.DataAnnotations;

namespace Qurrah.Integration.ServiceWrappers.DTOs.Authentication
{
    public class RegistrationRequest
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Validation.Required.GeneralErrorMessage")]
        [StringLength(150, ErrorMessage = "Validation.StringLength.150")]
        [Display(Name = "Registration.User.FirstName")]
        public string FirstName { get; set; }

        [StringLength(150, ErrorMessage = "Validation.StringLength.150")]
        [Display(Name = "Registration.User.SecondName")]
        public string SecondName { get; set; }

        [StringLength(150, ErrorMessage = "Validation.StringLength.150")]
        [Display(Name = "Registration.User.ThirdName")]
        public string ThirdName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Validation.Required.GeneralErrorMessage")]
        [StringLength(150, ErrorMessage = "Validation.StringLength.150")]
        [Display(Name = "Registration.User.FourthName")]
        public string FourthName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Validation.Required.GeneralErrorMessage")]
        [RegularExpression(@"0[1-9][0-9]{8}", ErrorMessage = "Validation.RegularExpression.10Digits")]
        [Display(Name = "Registration.User.MobileNumber")]
        public string MobileNumber { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Validation.Required.GeneralErrorMessage")]
        [Display(Name = "Registration.User.Gender")]
        public byte FKGenderId { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "Validation.Required.GeneralErrorMessage")]
        [Display(Name = "Registration.User.UserType")]
        public byte FKUserTypeId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Validation.Required.GeneralErrorMessage")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Validation.RegularExpression.10Digits")]
        [Display(Name = "Registration.User.IdNumber")]
        public string IdNumber { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Validation.Required.GeneralErrorMessage")]
        [Display(Name = "Registration.User.UserName")]
        public string UserName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Validation.Required.GeneralErrorMessage")]
        [Display(Name = "Registration.User.Email")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Validation.Required.GeneralErrorMessage")]
        [RegularExpression(@"0[1-9][0-9]{8}", ErrorMessage = "Validation.RegularExpression.10Digits")]
        [Display(Name = "Registration.User.PhoneNumber")]
        public string PhoneNumber { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Validation.Required.GeneralErrorMessage")]
        [Display(Name = "Registration.User.Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Validation.Required.GeneralErrorMessage")]
        [Display(Name = "Registration.User.ConfirmPassword")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Validation.Compare.NotMatchedPassword")]
        public string ConfirmPassword { get; set; }
    }
}