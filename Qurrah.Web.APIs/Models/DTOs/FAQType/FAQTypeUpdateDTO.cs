using System.ComponentModel.DataAnnotations;

namespace Qurrah.Web.APIs.Models.DTOs.FAQType
{
    public class FAQTypeUpdateDTO
    {
        [Required]
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(1000)]
        public string NameAr { get; set; }

        public string DescriptionAr { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(1000)]
        public string NameEn { get; set; }

        public string DescriptionEn { get; set; }
        
        [Required(AllowEmptyStrings = false)]
        [Range(1, 1000)]
        public int DisplayOrder { get; set; }
    }
}