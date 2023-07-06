using Qurrah.Integration.ServiceWrappers.DTOs.Localization;

namespace Qurrah.Integration.ServiceWrappers.DTOs.FAQ
{
    public class FAQTypeWithLocalizedProperties
    {
        public FAQType FAQType { get; set; }
        public List<LocalizedProperty> LocalizedProperties { get; set; }
    }
}