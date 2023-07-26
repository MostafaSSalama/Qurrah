using Microsoft.IdentityModel.Tokens;
using Qurrah.Data.Repository.IRepository;
using Qurrah.Entities;

namespace Qurrah.Data.Repository
{
    public class CenterRepository : Repository<Center>, ICenterRepository
    {
        #region Fields
        #endregion

        #region Ctor
        public CenterRepository(QurrahDbContext dbContext) : base(dbContext)
        {
        }
        #endregion

        #region Methods
        public async Task AddWithLocalizedPropertiesWithSaveAsync(Center center, List<LocalizedProperty> localizedProperties)
        {
            using var transaction = DbContext.Database.BeginTransaction();
            try
            {
                if (center.CenterLicenses.IsNullOrEmpty())
                    throw new Exception("Center licenses are required");

                center.FKCenterStatusId = CenterStatusId.UnderConsideration;
                center.StatusDate = DateTime.Now;
                center.CreatedOn = DateTime.Now;

                center.CenterLicenses.ForEach(license =>
                {
                    license.CreatedOn = DateTime.Now;
                    license.FKStatusId = CenterLicenseStatusId.UnderConsideration;
                    license.StatusDate = DateTime.Now;
                });

                await AddAsync(center);
                await DbContext.SaveChangesAsync();

                if (!localizedProperties.IsNullOrEmpty())
                {
                    localizedProperties.ForEach(lp =>
                    {
                        lp.EntityId = center.Id;
                        lp.Language = null;
                    });

                    await DbContext.LocalizedProperty.AddRangeAsync(localizedProperties);
                    await DbContext.SaveChangesAsync();
                }

                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction?.Rollback();
                throw;
            }
        }
        #endregion
    }
}