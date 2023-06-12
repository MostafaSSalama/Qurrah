using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Qurrah.Entities
{
    public class FAQType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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

        public List<FAQ> FAQs { get; set; }
    }
}