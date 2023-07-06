using Microsoft.EntityFrameworkCore;
using Qurrah.Data.Repository.IRepository;
using Qurrah.Entities;
using System.Linq;
using System.Linq.Expressions;

namespace Qurrah.Data.Repository
{
    public class FAQRepository : Repository<FAQ>, IFAQRepository
    {
        #region Ctor
        public FAQRepository(QurrahDbContext dbContext) : base(dbContext)
        {

        }
        #endregion

        #region Methods
        public async Task AddWithLocaliedPropertiesWithSaveAsync(FAQ faq, List<LocalizedProperty> localizedProperties)
        {
            using var transaction = DbContext.Database.BeginTransaction();
            try
            {
                await AddAsync(faq);
                await DbContext.SaveChangesAsync();

                if (localizedProperties?.Any() == true)
                {
                    localizedProperties.ForEach(lp =>
                    {
                        lp.EntityId = faq.Id;
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
        public async Task<FAQWithLocalizedProperties> SingleOrDefaultWithLocalizedProperties(Expression<Func<FAQ, bool>> filter, string includedProperties = null, bool tracked = true)
        {
            var query = IncludeProperties(includedProperties);
            if (!tracked)
                query = query.AsNoTracking();

            FAQWithLocalizedProperties faqWithLocalizedProps = new();
            var faq = await query.SingleOrDefaultAsync(filter);
            if (null != faq)
            {
                faqWithLocalizedProps.FAQ = faq;
                faqWithLocalizedProps.LocalizedProperties = DbContext.LocalizedProperty.Where(lp => lp.EntityId == faq.Id);
            }

            return faqWithLocalizedProps;
        }
        public async Task<ActionResult> RemoveWithLocalizedPropertiesWithSaveAsync(int id)
        {
            ActionResult actionResult;
            using var transaction = DbContext.Database.BeginTransaction();
            try
            {
                var faq = await SingleOrDefaultAsync(t => t.Id == id);
                if (null == faq)
                    actionResult = ActionResult.ItemNotFound;
                else
                {
                    var localizedProps = DbContext.LocalizedProperty.Where(lp => lp.EntityId == id && lp.LocaleKeyGroup == nameof(FAQ));
                    if (localizedProps?.Any() == true)
                        DbContext.LocalizedProperty.RemoveRange(localizedProps);

                    Remove(faq);

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
        public async Task<ActionResult> UpdateWithLocalizedPropertiesWithSaveAsync(FAQ faq, List<LocalizedProperty> localizedProperties)
        {
            ActionResult actionResult;
            using var transaction = DbContext.Database.BeginTransaction();
            try
            {
                var faqTypeEntity = await SingleOrDefaultAsync(t => t.Id == faq.Id, tracked: false);
                if (null == faqTypeEntity)
                    actionResult = ActionResult.ItemNotFound;
                else
                {
                    Update(faq);

                    //Remove
                    var localizedPropsToRemove = localizedProperties.Where(lp => string.IsNullOrWhiteSpace(lp.LocaleValue?.Trim())
                                                                              && lp.EntityId == faq.Id
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
                                                                              && lp.EntityId == faq.Id
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
                            lp.EntityId = faq.Id;
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
        public async Task<IEnumerable<FAQClassifiedWithLocalizedProperties>> GetAllClassifiedByTypeAsync()
        {
            var faqTypesWithLocalizedProps = DbContext.FAQType
                                                      .GroupJoin(DbContext.LocalizedProperty
                                                                          .Include(lp => lp.Language)
                                                                          .Where(lp => lp.LocaleKeyGroup == nameof(FAQType)),
                                                                 faqType => faqType.Id,
                                                                 lp => lp.EntityId,
                                                                 (faqType, lp) => new
                                                                 {
                                                                     FAQType = faqType,
                                                                     LocalizedProperty = lp
                                                                 })
                                                      .SelectMany(lps => lps.LocalizedProperty.DefaultIfEmpty(),
                                                                  (f, lp) => new
                                                                  {
                                                                      f.FAQType,
                                                                      LocalizedProperty = lp
                                                                  })
                                                      .GroupBy(g => g.FAQType)
                                                      .Select(g => new FAQTypeWithLocalizedProperties
                                                      {
                                                          FAQType = g.Key,
                                                          LocalizedProperties = g.Select(g => g.LocalizedProperty)
                                                      }).ToListAsync().GetAwaiter().GetResult();

            var faqsWithLocalizedProps = DbContext.FAQ
                                                  .GroupJoin(DbContext.LocalizedProperty
                                                                      .Include(lp => lp.Language)
                                                                      .Where(lp => lp.LocaleKeyGroup == nameof(FAQ)),
                                                             faq => faq.Id,
                                                             lp => lp.EntityId,
                                                             (faq, lp) => new
                                                             {
                                                                 FAQ = faq,
                                                                 LocalizedProperty = lp
                                                             })
                                                  .SelectMany(lps => lps.LocalizedProperty.DefaultIfEmpty(),
                                                             (f, lp) => new
                                                             {
                                                                 f.FAQ,
                                                                 LocalizedProperty = lp
                                                             })
                                                  .GroupBy(g => g.FAQ)
                                                  .Select(g => new FAQWithLocalizedProperties
                                                  {
                                                      FAQ = g.Key,
                                                      LocalizedProperties = g.Select(g => g.LocalizedProperty)
                                                  }).ToListAsync().GetAwaiter().GetResult();

            var result = faqTypesWithLocalizedProps.Join(faqsWithLocalizedProps, 
                                                               t => t.FAQType.Id,
                                                               f => f.FAQ.FKTypeId,
                                                               (t, f) => new
                                                               {
                                                                   FAQTypeWithLocalizedProperties = t,
                                                                   FAQWithLocalizedProperties = f
                                                               })
                                                         .GroupBy(f => f.FAQTypeWithLocalizedProperties)
                                                         .Select(g => new FAQClassifiedWithLocalizedProperties
                                                         {
                                                             Type = g.Key,
                                                             FAQs = g.Select(g => g.FAQWithLocalizedProperties)
                                                         })
                                                         .ToList();
            return result;
        }
        #endregion
    }
}