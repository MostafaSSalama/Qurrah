using Qurrah.Entities;

namespace Qurrah.Data.Repository
{
    public class FAQTypeRepository : Repository<FAQType>
    {
        public FAQTypeRepository(QurrahDbContext dbContext) : base(dbContext)
        {
             
        }
        public override void Update(FAQType faqType)
        {
            base.Update(faqType);
        }
    }
}