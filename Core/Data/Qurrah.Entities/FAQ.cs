using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Qurrah.Entities
{
    public class FAQ
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(AllowEmptyStrings =false)]
        [StringLength(1000)]
        public string Question { get; set; }
        
        [Required(AllowEmptyStrings = false)]
        public string Answer { get; set; }
        
        [Required]
        [ForeignKey("FAQType")]
        public int FKTypeId { get; set; }

        [ValidateNever]
        public FAQType FAQType { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Range(1, 1000)]
        public int DisplayOrder { get; set; }
    }
}