using Qurrah.Models.Integration.DTOs.FAQType;
using System.ComponentModel.DataAnnotations;

namespace Qurrah.Models.Integration.DTOs.FAQ
{
    public class FAQDTO
    {
        public int Id { get; set; }

        [Display(Name = "FAQ.Create.QuestionEn")]
        public string QuestionEn { get; set; }

        [Display(Name = "FAQ.Create.AnswerEn")]
        public string AnswerEn { get; set; }

        [Display(Name = "FAQ.Create.QuestionAr")]
        public string QuestionAr { get; set; }
        
        [Display(Name = "FAQ.Create.AnswerAr")]
        public string AnswerAr { get; set; }

        [Display(Name = "FAQ.Create.FKTypeId")]
        public int FKTypeId { get; set; }
        public FAQTypeDTO FAQType { get; set; }

        [Display(Name = "FAQ.Create.DisplayOrder")]
        public int DisplayOrder { get; set; }
    }
}