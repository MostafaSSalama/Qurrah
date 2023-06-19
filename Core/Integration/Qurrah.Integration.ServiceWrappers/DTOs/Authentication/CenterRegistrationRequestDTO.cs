
using System.ComponentModel.DataAnnotations;

namespace Qurrah.Integration.ServiceWrappers.DTOs.Authentication
{
    public class CenterUserRegistrationRequestDTO
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Validation.Required.GeneralErrorMessage")]
        [Display(Name = "Registration.CenterUser.CenterName")]
        public string CenterName { get; set; }

        [Required(AllowEmptyStrings = false)]
        public CenterOwnerDTO CenterOwner { get; set; }
    }
}