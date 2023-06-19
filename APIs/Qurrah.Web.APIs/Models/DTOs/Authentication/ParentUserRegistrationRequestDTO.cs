using System.ComponentModel.DataAnnotations;

namespace Qurrah.Web.APIs.Models.DTOs.Authentication
{
    public class ParentUserRegistrationRequestDTO
    {
        [Required(AllowEmptyStrings = false)]
        [StringLength(150)]
        public string FirstName { get; set; }

        [StringLength(150)]
        public string SecondName { get; set; }

        [StringLength(150)]
        public string ThirdName { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(150)]
        public string FourthName { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(10)]
        [RegularExpression(@"0[1-9][0-9]{8}")]
        public string MobileNumber { get; set; }

        [Required(AllowEmptyStrings = false)]
        public byte FKGenderId { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(10)]
        [RegularExpression(@"^\d{10}$")]
        public string IdNumber { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string UserName { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(10)]
        [RegularExpression(@"0[1-9][0-9]{8}")]
        public string PhoneNumber { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Password { get; set; }
    }
}