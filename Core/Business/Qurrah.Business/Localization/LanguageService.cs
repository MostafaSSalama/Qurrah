using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;

namespace Qurrah.Business.Localization
{
    public class LanguageService
    {
        #region Fields
        private readonly IStringLocalizer _localizer;
        #endregion

        #region Properties
        public SupportedLanguage CurrentLanguage
        {
            get
            {
                string culture = Thread.CurrentThread.CurrentUICulture.Name.ToLower();
                switch (culture)
                {
                    case "en-us":
                        return SupportedLanguage.English;
                    case "ar-sa":
                    default:
                        return SupportedLanguage.Arabic;
                }
            }
        }
        #endregion

        #region Ctor
        public LanguageService(IStringLocalizerFactory factory, IConfiguration configuration)
        {
            var assemblyName = configuration.GetValue<string>("AppSettings:AssemblyName");
            _localizer = factory.Create("SharedResource", assemblyName);
        }
        #endregion

        #region Methods
        public LocalizedString GetLocalizedString(string key)
        {
            return _localizer[key];
        }
        #endregion
    }
}