using Microsoft.AspNetCore.Identity;
using Qurrah.Entities.NoMapping;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Qurrah.Entities
{
    public class ApplicationUser : IdentityUser
    {
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
    }
}