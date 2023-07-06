namespace Qurrah.Web.APIs.Models.DTOs.Localization
{
    public class LocaleDTO
    {
        public int LanguageId { get; set; }
        public string LanguageName { get; set; }
        public string Description { get; set; }
        public string LanguageCulture { get; set; }
    }
}