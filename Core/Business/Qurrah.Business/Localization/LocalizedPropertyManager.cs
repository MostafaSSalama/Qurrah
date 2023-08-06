using Qurrah.Business.Localization.Entities;
using Qurrah.Business.Logging;
using Qurrah.Integration.ServiceWrappers;
using Qurrah.Integration.ServiceWrappers.DTOs.Localization;

namespace Qurrah.Business.Localization
{
    public class LocalizedPropertyManager: ILocalizedPropertyManager
    {
        #region Fields
        private readonly LanguageService _localization;
        private readonly IExceptionLogging _exceptionLogging;
        #endregion

        #region Ctor
        public LocalizedPropertyManager(LanguageService localization, IExceptionLogging exceptionLogging)
        {
            _localization = localization;
            _exceptionLogging = exceptionLogging;
        }
        #endregion

        #region Methods
        public List<LocalizedPropertyGroup> PopulateLocalizedPropertyGroups(Type type, List<LocaleInfo> locales)
        {
            var localizedPropertyGroups = new List<LocalizedPropertyGroup>();
            try
            {
                if (locales?.Any() == true)
                {
                    var localizedPropAttributes = type.GetProperties()
                                                      .Where(prop => prop.IsDefined(typeof(LocalizedAttribute), false))
                                                      .Select(p => new
                                                      {
                                                          Attribute = (Attribute.GetCustomAttribute(p, typeof(LocalizedAttribute)) as LocalizedAttribute)
                                                      })
                                                      .ToList();

                    if (localizedPropAttributes?.Any() == true)
                    {
                        locales.ForEach(locale =>
                        {
                            localizedPropertyGroups.Add(new LocalizedPropertyGroup
                            {
                                LocaleId = locale.LanguageId,
                                LocalizedProperties = localizedPropAttributes.Select(p => new Entities.LocalizedProperty
                                {
                                    LanguageId = locale.LanguageId,
                                    LocaleKeyGroup = p.Attribute.LocaleKeyGroup,
                                    LocaleKey = p.Attribute.LocaleKey,
                                    Label = _localization.GetLocalizedString(p.Attribute.DisplayValue),
                                    IsMultiLine = p.Attribute.IsMultiLine
                                }).ToList()
                            });
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                _exceptionLogging.Log(ex);
            }
            return localizedPropertyGroups;
        }
        #endregion
    }
}