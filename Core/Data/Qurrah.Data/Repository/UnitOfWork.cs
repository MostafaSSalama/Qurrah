using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Qurrah.Data.Repository.IRepository;
using Qurrah.Entities;

namespace Qurrah.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Fields
        private readonly QurrahDbContext _dbContext;
        #endregion

        #region Properties
        public FAQTypeRepository FAQType { get; private set; }
        public FAQRepository FAQ { get; private set; }
        public ParentUserRepository ParentUser { get; private set; }
        public CenterUserRepository CenterUser { get; private set; }
        #endregion

        #region Constructors
        public UnitOfWork(QurrahDbContext dbContext, UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _dbContext = dbContext;
            FAQType = new FAQTypeRepository(dbContext);
            FAQ = new FAQRepository(dbContext);
            ParentUser = new ParentUserRepository(dbContext, userManager, configuration);
            CenterUser = new CenterUserRepository(dbContext, userManager, configuration);
        }
        #endregion

        #region Methods
        //It is better to have Save method on the top level of Repository Pattern - here
        //To do only one visit to the database to save all changes on the level of the current request
        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
        #endregion
    }
}