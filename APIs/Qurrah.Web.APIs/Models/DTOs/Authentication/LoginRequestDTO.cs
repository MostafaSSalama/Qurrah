using System.ComponentModel.DataAnnotations;

namespace Qurrah.Web.APIs.Models.DTOs.Authentication
{
    public class LoginRequestDTO
    {
        [Required(AllowEmptyStrings = false)]
        public string UserName { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Password { get; set; }
    }
}