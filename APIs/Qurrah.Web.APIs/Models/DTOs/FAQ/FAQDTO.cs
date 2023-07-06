using Qurrah.Web.APIs.Models.DTOs.FAQType;

namespace Qurrah.Web.APIs.Models.DTOs.FAQ
{
    public class FAQDTO
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public int FKTypeId { get; set; }
        public FAQTypeDTO FAQType { get; set; }
        public int DisplayOrder { get; set; }
    }
}