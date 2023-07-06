using Qurrah.Entities;

namespace Qurrah.Data.Repository.IRepository
{
    public interface ILanguageDescriptionRepository
    {
        Task<IEnumerable<LocaleInfo>> GetLocales(string culture);
    }
}