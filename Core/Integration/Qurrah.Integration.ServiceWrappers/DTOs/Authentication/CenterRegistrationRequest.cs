
using System.ComponentModel.DataAnnotations;

namespace Qurrah.Integration.ServiceWrappers.DTOs.Authentication
{
    public class CenterUserRegistrationRequest
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Validation.Required.GeneralErrorMessage")]
        [Display(Name = "Registration.CenterUser.CenterName")]
        public string CenterName { get; set; }

        [Required(AllowEmptyStrings = false)]
        public CenterOwner CenterOwner { get; set; }
    }
}