using Microsoft.EntityFrameworkCore;
using Qurrah.Data.Repository.IRepository;
using Qurrah.Entities;
using System.Linq.Expressions;

namespace Qurrah.Data.Repository
{
    public class FAQTypeRepository : Repository<FAQType>, IFAQTypeRepository
    {
        #region Ctor
        public FAQTypeRepository(QurrahDbContext dbContext) : base(dbContext)
        {

        }
        #endregion

        #region Methods
        public async Task AddWithLocaliedPropertiesWithSaveAsync(FAQType faqType, List<LocalizedProperty> localizedProperties)
        {
            using var transaction = DbContext.Database.BeginTransaction();
            try
            {
                await AddAsync(faqType);
                await DbContext.SaveChangesAsync();

                if (localizedProperties?.Any() == true)
                {
                    localizedProperties.ForEach(lp =>
                    {
                        lp.EntityId = faqType.Id;
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
        public async Task<FAQTypeWithLocalizedProperties> SingleOrDefaultWithLocalizedProperties(Expression<Func<FAQType, bool>> filter, string includedProperties = null, bool tracked = true)
        {
            var query = IncludeProperties(includedProperties);
            if (!tracked)
                query = query.AsNoTracking();

            FAQTypeWithLocalizedProperties faqTypeWithLocalizedProps = new();
            var faqType = await query.SingleOrDefaultAsync(filter);
            if (null != faqType)
            {
                faqTypeWithLocalizedProps.FAQType = faqType;
                faqTypeWithLocalizedProps.LocalizedProperties = DbContext.LocalizedProperty.Where(lp => lp.EntityId == faqType.Id);
            }

            return faqTypeWithLocalizedProps;
        }
        public async Task<ActionResult> RemoveWithLocalizedPropertiesWithSaveAsync(int id)
        {
            ActionResult actionResult;
            using var transaction = DbContext.Database.BeginTransaction();
            try
            {
                var faqType = await SingleOrDefaultAsync(t => t.Id == id);
                if (null == faqType)
                    actionResult = ActionResult.ItemNotFound;
                else
                {
                    var localizedProps = DbContext.LocalizedProperty.Where(lp => lp.EntityId == id && lp.LocaleKeyGroup == nameof(FAQType));
                    if (localizedProps?.Any() == true)
                        DbContext.LocalizedProperty.RemoveRange(localizedProps);

                    Remove(faqType);

                    await DbContext.SaveChangesAsync();
                    transaction.Commit();
                    actionResult = ActionResult.Success;
                }
            }
            catch (Exception ex)
            {
                transaction?.Rollback();
                throw;
            }

            return actionResult;
        }
        public async Task<ActionResult> UpdateWithLocalizedPropertiesWithSaveAsync(FAQType faqType, List<LocalizedProperty> localizedProperties)
        {
            ActionResult actionResult;
            using var transaction = DbContext.Database.BeginTransaction();
            try
            {
                var faqTypeEntity = await SingleOrDefaultAsync(t => t.Id == faqType.Id, tracked: false);
                if (null == faqTypeEntity)
                    actionResult = ActionResult.ItemNotFound;
                else
                {
                    Update(faqType);
                    
                    //Remove
                    var localizedPropsToRemove = localizedProperties.Where(lp => string.IsNullOrWhiteSpace(lp.LocaleValue?.Trim())
                                                                              && lp.EntityId == faqType.Id
                                                                              && lp.Id > 0)
                                                                    .ToList();
                    if (localizedPropsToRemove?.Any() == true)
                    {
                        localizedPropsToRemove.ForEach(lp =>
                        {
                            lp.Language = null;
                        });
                        DbContext.LocalizedProperty.RemoveRange(localizedPropsToRemove);
                    }

                    //Update
                    var localizedPropsToUpdate = localizedProperties.Where(lp => !string.IsNullOrWhiteSpace(lp.LocaleValue?.Trim())
                                                                              && lp.EntityId == faqType.Id
                                                                              && lp.Id > 0)
                                                                    .ToList();
                    if (localizedPropsToUpdate?.Any() == true)
                    {
                        localizedPropsToUpdate.ForEach(lp =>
                        {
                            lp.Language = null;
                        });
                        DbContext.LocalizedProperty.UpdateRange(localizedPropsToUpdate);
                    }

                    //Add
                    var localizedPropsToAdd = localizedProperties.Where(lp => !string.IsNullOrWhiteSpace(lp.LocaleValue?.Trim())
                                                                           && lp.EntityId <= 0)
                                                                 .ToList();
                    if (localizedPropsToAdd?.Any() == true)
                    {
                        localizedPropsToAdd.ForEach(lp =>
                        {
                            lp.EntityId = faqType.Id;
                            lp.Language = null;
                        });
                        await DbContext.LocalizedProperty.AddRangeAsync(localizedPropsToAdd);
                    }

                    await DbContext.SaveChangesAsync();
                    transaction.Commit();

                    actionResult = ActionResult.Success;
                }
            }
            catch (Exception ex)
            {
                transaction?.Rollback();
                throw;
            }

            return actionResult;
        }
        #endregion
    }
}