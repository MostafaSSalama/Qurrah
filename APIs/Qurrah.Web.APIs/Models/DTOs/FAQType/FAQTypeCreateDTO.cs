using System.ComponentModel.DataAnnotations;

namespace Qurrah.Web.APIs.Models.DTOs.FAQType
{
    public class FAQTypeCreateDTO
    {
        [Required(AllowEmptyStrings = false)]
        [StringLength(1000)]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Range(1, 1000)]
        public int DisplayOrder { get; set; }
    }
}