using Microsoft.EntityFrameworkCore;
using Qurrah.Data.Repository.IRepository;
using Qurrah.Entities;

namespace Qurrah.Data.Repository
{
    public class CenterTypeDescriptionRepository : ICenterTypeDescriptionRepository
    {
        #region Fields
        private readonly QurrahDbContext _dbContext;
        #endregion

        #region Ctor
        public CenterTypeDescriptionRepository(QurrahDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        #region Methods
        public async Task<IEnumerable<LookupInfo>> GetAllCenterTypes(string culture)
        {
            var result = await _dbContext.CenterTypeDescription
                                         .Include(ctd => ctd.Language)
                                         .Include(ctd => ctd.CenterType)
                                         .Where(ctd => ctd.Language.LanguageCulture.Trim().ToLower() == culture.Trim().ToLower())
                                         .OrderBy(ctd => ctd.Description)
                                         .Select(ctd => new LookupInfo
                                         {
                                             Id = (int)ctd.CenterType.Id,
                                             Text = ctd.Description
                                         })
                                         .ToListAsync();
            return result;
        }
        #endregion
    }
}