using System.ComponentModel.DataAnnotations;

namespace Qurrah.Entities.NoMapping
{
    public class LoginRequest
    {
        [Required(AllowEmptyStrings = false)]
        public string UserName { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Password { get; set; }
    }
}