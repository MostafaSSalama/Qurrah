using Qurrah.Integration.ServiceWrappers.DTOs.Localization;
namespace Qurrah.Integration.ServiceWrappers.DTOs.Center
{
    public class CenterWithLocalizedProperties
    {
        public Center Center { get; set; }
        public List<LocalizedProperty> LocalizedProperties { get; set; }
    }
}