using Microsoft.EntityFrameworkCore;
using Qurrah.Entities;

namespace Qurrah.Data.Repository
{
    public class FAQRepository : Repository<FAQ>
    {
        public FAQRepository(QurrahDbContext dbContext) : base(dbContext)
        {

        }
        public async Task<IEnumerable<FAQClassified>> GetAllClassifiedByTypeAsync()
        {
            return await DbContext.FAQ.Join(DbContext.FAQType, q => q.FKTypeId, t => t.Id, (q, t) => new { Type = t, FAQ = q })
                                  .GroupBy(g => g.Type)
                                  .OrderBy(g => g.Key.DisplayOrder)
                                  .Select(g => new FAQClassified
                                  {
                                      Type = g.Key,
                                      FAQs = g.Select(g => g.FAQ)
                                  })
                                  .ToListAsync();
        }
        public override void Update(FAQ faq)
        {
            base.Update(faq);
        }
    }
}