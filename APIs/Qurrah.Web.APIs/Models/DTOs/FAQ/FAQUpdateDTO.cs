using System.ComponentModel.DataAnnotations;

namespace Qurrah.Web.APIs.Models.DTOs.FAQ
{
    public class FAQUpdateDTO
    {
        [Required]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(1000)]
        public string Question { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Answer { get; set; }

        [Required]
        public int FKTypeId { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Range(1, 1000)]
        public int DisplayOrder { get; set; }
    }
}