using Qurrah.Entities;
using System.Linq.Expressions;

namespace Qurrah.Data.Repository.IRepository
{
    public interface IFAQTypeRepository
    {
        Task AddWithLocaliedPropertiesWithSaveAsync(FAQType faqType, List<LocalizedProperty> localizedProperties);
        Task<ActionResult> UpdateWithLocalizedPropertiesWithSaveAsync(FAQType faqType, List<LocalizedProperty> localizedProperties);
        Task<FAQTypeWithLocalizedProperties> SingleOrDefaultWithLocalizedProperties(Expression<Func<FAQType, bool>> filter, string includedProperties = null, bool tracked = true);
        Task<ActionResult> RemoveWithLocalizedPropertiesWithSaveAsync(int id);
    }
}
