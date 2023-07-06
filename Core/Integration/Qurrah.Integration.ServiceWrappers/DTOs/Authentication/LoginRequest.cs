using System.ComponentModel.DataAnnotations;

namespace Qurrah.Integration.ServiceWrappers.DTOs.Authentication
{
    public class LoginRequest
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Validation.Required.GeneralErrorMessage")]
        [Display(Name = "Registration.Login.UserNameOrEmail")]
        public string UserName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Validation.Required.GeneralErrorMessage")]
        [Display(Name = "Registration.Login.Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Registration.Login.RememberMe")]
        public bool RememberMe { get; set; }
    }
}