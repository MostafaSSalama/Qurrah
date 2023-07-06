using Qurrah.Integration.ServiceWrappers.DTOs.Localization;

namespace Qurrah.Integration.ServiceWrappers.DTOs.FAQ
{
    public class FAQWithLocalizedProperties
    {
        public FAQ FAQ { get; set; }
        public List<LocalizedProperty> LocalizedProperties { get; set; }
    }
}