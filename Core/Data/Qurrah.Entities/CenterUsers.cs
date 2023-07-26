using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Qurrah.Entities
{
    public class CenterUser
    {
        [Required(AllowEmptyStrings = false)]
        [ForeignKey(nameof(User))]
        public string FKUserId { get; set; }
        public ApplicationUser User { get; set; }

        [Required]
        [ForeignKey(nameof(Center))]
        public int FKCenterId { get; set; }
        public Center Center { get; set; }
    }
}