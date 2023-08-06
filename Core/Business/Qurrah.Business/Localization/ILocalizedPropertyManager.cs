using Qurrah.Business.Localization.Entities;
using Qurrah.Integration.ServiceWrappers.DTOs.Localization;

namespace Qurrah.Business.Localization
{
    public interface ILocalizedPropertyManager
    {
        List<LocalizedPropertyGroup> PopulateLocalizedPropertyGroups(Type type, List<LocaleInfo> locales);
    }
}