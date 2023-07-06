using Qurrah.Entities;

namespace Qurrah.Data.Repository
{
    public class LocalizedPropertyRepository : Repository<LocalizedProperty>
    {
        public LocalizedPropertyRepository(QurrahDbContext dbContext) : base(dbContext)
        {

        }
        
        public override void Update(LocalizedProperty localizedProperty)
        {
            base.Update(localizedProperty);
        }
    }
}