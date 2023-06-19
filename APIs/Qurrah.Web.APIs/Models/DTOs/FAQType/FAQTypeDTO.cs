using System.ComponentModel.DataAnnotations;

namespace Qurrah.Web.APIs.Models.DTOs.FAQType
{
    public class FAQTypeDTO
    {
        public int Id { get; set; }
        public string NameAr { get; set; }
        public string DescriptionAr { get; set; }
        public string NameEn { get; set; }
        public string DescriptionEn { get; set; }
        public int DisplayOrder { get; set; }
    }
}