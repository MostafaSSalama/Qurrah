using Qurrah.Integration.ServiceWrappers.DTOs.Localization;

namespace Qurrah.Web.Models
{
    public class LocalizedPropertyGroupVM
    {
        public int LocaleId { get; set; }
        public List<LocalizedProperty> LocalizedProperties { get; set; }
    }
}