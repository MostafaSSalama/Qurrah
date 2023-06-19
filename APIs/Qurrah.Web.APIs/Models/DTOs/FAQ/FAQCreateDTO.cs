using System.ComponentModel.DataAnnotations;

namespace Qurrah.Web.APIs.Models.DTOs.FAQ
{
    public class FAQCreateDTO
    {
        [Required(AllowEmptyStrings = false)]
        [StringLength(1000)]
        public string QuestionEn { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string AnswerEn { get; set; }

        [StringLength(1000)]
        [Required(AllowEmptyStrings = false)]
        public string QuestionAr { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string AnswerAr { get; set; }

        [Required]
        public int FKTypeId { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Range(1, 1000)]
        public int DisplayOrder { get; set; }
    }
}