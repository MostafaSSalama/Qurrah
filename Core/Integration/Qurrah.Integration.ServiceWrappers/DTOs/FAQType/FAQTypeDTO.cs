using System.ComponentModel.DataAnnotations;

namespace Qurrah.Integration.ServiceWrappers.DTOs.FAQType
{
    public class FAQTypeDTO
    {
        public int Id { get; set; }

        [Display(Name = "FAQType.Create.NameAr")]
        public string NameAr { get; set; }

        [Display(Name = "FAQType.Create.DescriptionAr")]
        public string DescriptionAr { get; set; }

        [Display(Name = "FAQType.Create.NameEn")]
        public string NameEn { get; set; }

        [Display(Name = "FAQType.Create.DescriptionEn")]
        public string DescriptionEn { get; set; }

        [Display(Name = "FAQType.Create.DisplayOrder")]
        public int DisplayOrder { get; set; }
    }
}