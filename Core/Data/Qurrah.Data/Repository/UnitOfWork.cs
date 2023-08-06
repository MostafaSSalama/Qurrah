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
        public ApplicationUserRepository ApplicationUser { get; private set; }
        public LanguageDescriptionRepository LanguageDescription { get; private set; }
        public GenderDescriptionRepository GenderDescription { get; private set; }
        public UserTypeDescriptionRepository UserTypeDescription { get; private set; }
        public CenterTypeDescriptionRepository CenterTypeDescription { get; private set; }
        public FileRepository File { get; private set; }
        public CenterRepository Center { get; private set; }

        #endregion

        #region Constructors
        public UnitOfWork(QurrahDbContext dbContext, UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _dbContext = dbContext;
            FAQType = new FAQTypeRepository(dbContext);
            FAQ = new FAQRepository(dbContext);
            ApplicationUser = new ApplicationUserRepository(dbContext, userManager, configuration);
            LanguageDescription = new LanguageDescriptionRepository(dbContext);
            GenderDescription = new GenderDescriptionRepository(dbContext);
            UserTypeDescription = new UserTypeDescriptionRepository(dbContext);
            CenterTypeDescription = new CenterTypeDescriptionRepository(dbContext);
            File = new FileRepository(dbContext);
            Center = new CenterRepository(dbContext);
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