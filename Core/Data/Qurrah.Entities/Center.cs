using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Qurrah.Entities
{
    public class Center
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(AllowEmptyStrings = false)]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
    }
}