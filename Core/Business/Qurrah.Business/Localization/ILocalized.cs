using Qurrah.Integration.ServiceWrappers.DTOs.Localization;

namespace Qurrah.Business.Localization
{
    public interface ILocalized
    {
        APIResult PopulateDefaultLocalizedPropertyGroups(List<LocaleInfo> locales);
        APIResult PopulateLocalizedPropertyGroups(List<LocalizedProperty> localizedProperties, List<LocaleInfo> locales, bool isReadonly);
    }
}