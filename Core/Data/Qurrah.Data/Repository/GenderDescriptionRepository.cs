using Microsoft.EntityFrameworkCore;
using Qurrah.Data.Repository.IRepository;
using Qurrah.Entities;

namespace Qurrah.Data.Repository
{
    public class GenderDescriptionRepository : IGenderDescriptionRepository
    {
        #region Fields
        private readonly QurrahDbContext _dbContext;
        #endregion

        #region Ctor
        public GenderDescriptionRepository(QurrahDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        #region Methods
        public async Task<IEnumerable<LookupInfo>> GetAllGenders(string culture)
        {
            var result = await _dbContext.GenderDescription
                                         .Include(gd => gd.Language)
                                         .Include(gd => gd.Gender)
                                         .Where(gd => gd.Language.LanguageCulture.Trim().ToLower() == culture.Trim().ToLower())
                                         .OrderByDescending(gd => gd.Description)
                                         .Select(gd => new LookupInfo
                                         {
                                             Id = (int)gd.Gender.Id,
                                             Text = gd.Description
                                         })
                                         .ToListAsync();
            return result;
        }
        #endregion
    }
}