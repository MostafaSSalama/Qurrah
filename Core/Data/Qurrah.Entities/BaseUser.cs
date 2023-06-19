using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Qurrah.Entities.NoMapping;

namespace Qurrah.Entities
{
    public class BaseUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

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
        [ForeignKey("Gender")]
        public GenderId FKGenderId { get; set; }

        public Gender Gender { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(10)]
        [RegularExpression(@"^\d{10}$")]
        public string IdNumber { get; set; }

        [ForeignKey("ApplicationUser")]
        [Required(AllowEmptyStrings = false)]
        public string FKUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
    }
}