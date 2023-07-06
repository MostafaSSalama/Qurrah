using Microsoft.EntityFrameworkCore;
using Qurrah.Data.Repository.IRepository;
using Qurrah.Entities;

namespace Qurrah.Data.Repository
{
    public class LanguageDescriptionRepository : ILanguageDescriptionRepository
    {
        #region Fields
        private readonly QurrahDbContext _dbContext;
        #endregion

        #region Ctor
        public LanguageDescriptionRepository(QurrahDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        #region Methods
        public async Task<IEnumerable<LocaleInfo>> GetLocales(string culture)
        {
            var result = await _dbContext.LanguageDescription
                                         .Include(ld => ld.Language)
                                         .Where(ld => ld.Language.Published
                                                        && ld.InLanguage.LanguageCulture.Trim().ToLower() == culture.Trim().ToLower())
                                         .OrderBy(ld => ld.Language.DisplayOrder)
                                         .Select(ld => new LocaleInfo
                                         {
                                             LanguageId = (int)ld.Language.Id,
                                             LanguageName = ld.Language.Name,
                                             Description = ld.Description,
                                             LanguageCulture = ld.Language.LanguageCulture
                                         })
                                         .ToListAsync();
            return result;
        }
        #endregion
    }
}