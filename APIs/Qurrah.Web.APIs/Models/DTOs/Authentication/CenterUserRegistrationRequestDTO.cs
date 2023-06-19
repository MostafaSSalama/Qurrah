using System.ComponentModel.DataAnnotations;

namespace Qurrah.Web.APIs.Models.DTOs.Authentication
{
    public class CenterUserRegistrationRequestDTO
    {
        [Required(AllowEmptyStrings = false)]
        public string CenterName { get; set; }

        [Required(AllowEmptyStrings = false)]
        public CenterOwnerDTO CenterOwner { get; set; }
    }
}