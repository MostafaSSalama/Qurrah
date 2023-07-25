using Microsoft.EntityFrameworkCore;
using Qurrah.Data.Repository.IRepository;
using Qurrah.Entities;

namespace Qurrah.Data.Repository
{
    public class UserTypeDescriptionRepository : IUserTypeDescriptionRepository
    {
        #region Fields
        private readonly QurrahDbContext _dbContext;
        #endregion

        #region Ctor
        public UserTypeDescriptionRepository(QurrahDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        #region Methods
        public async Task<IEnumerable<LookupInfo>> GetAllUserTypes(string culture)
        {
            var result = await _dbContext.UserTypeDescription
                                         .Include(utd => utd.Language)
                                         .Include(utd => utd.UserType)
                                         .Where(utd => utd.Language.LanguageCulture.Trim().ToLower() == culture.Trim().ToLower())
                                         .OrderBy(utd => utd.Description)
                                         .Select(utd => new LookupInfo
                                         {
                                             Id = (int)utd.UserType.Id,
                                             Text = utd.Description
                                         })
                                         .ToListAsync();
            return result;
        }
        #endregion
    }
}