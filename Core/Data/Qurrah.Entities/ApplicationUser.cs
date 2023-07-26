using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Qurrah.Entities
{
    public class ApplicationUser : IdentityUser
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
        [RegularExpression(@"^\d{10}$")]
        public string IdNumber { get; set; }

        [Required(AllowEmptyStrings = false)]
        [ForeignKey("Gender")]
        public GenderId FKGenderId { get; set; }
        public Gender Gender { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(10)]
        [RegularExpression(@"0[1-9][0-9]{8}")]
        public string MobileNumber { get; set; }

        [Required(AllowEmptyStrings = false)]
        [ForeignKey("UserType")]
        public UserTypeId FKUserTypeId { get; set; }
        public UserType UserType { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [ForeignKey("CreatedBy")]
        public string FKCreatedByUserId { get; set; }
        public ApplicationUser CreatedBy { get; set; }

        public DateTime? LastModifiedOn { get; set; }

        public List<Center> CentersCreatedBy { get; set; }
        public List<Center> CentersUpdatedBy { get; set; }
        public List<CenterUser> CenterUsers { get; set; }
        public List<CenterLicense> CenterLicenses { get; set; }
    }
}