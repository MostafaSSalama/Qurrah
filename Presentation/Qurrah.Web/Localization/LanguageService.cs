using Microsoft.Extensions.Localization;
using System.Reflection;

namespace Localization.Services
{
    /// <summary>
    /// Dummy class to group shared resources
    /// </summary>
    public class SharedResource { }

    public class LanguageService
    {
        private readonly IStringLocalizer _localizer;
        public LanguageService(IStringLocalizerFactory factory)
        {
            var type = typeof(SharedResource);
            var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
            _localizer = factory.Create("SharedResource", assemblyName.Name);
        }
        public LocalizedString GetLocalizedString(string key)
        {
            return _localizer[key];
        }
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
    }
    public enum SupportedLanguage
    {
        Arabic,
        English
    }
}