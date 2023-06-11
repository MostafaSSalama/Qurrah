using Qurrah.Web.APIs.Models.DTOs.FAQType;

namespace Qurrah.Web.APIs.Models.DTOs.FAQ
{
    public class FAQDTO
    {
        public int Id { get; set; }
        public string QuestionEn { get; set; }
        public string AnswerEn { get; set; }
        public string QuestionAr { get; set; }
        public string AnswerAr { get; set; }
        public int FKTypeId { get; set; }
        public FAQTypeDTO FAQType { get; set; }
        public int DisplayOrder { get; set; }
    }
}