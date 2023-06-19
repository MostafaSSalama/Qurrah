using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Qurrah.Entities
{
    public class CenterOwnerUser : BaseUser
    {
        [Required(AllowEmptyStrings = false)]
        [ForeignKey("Center")]
        public int FKCenterId { get; set; }

        public Center Center { get; set; }
    }
}