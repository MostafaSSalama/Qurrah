using Qurrah.Entities;
using System.Linq.Expressions;

namespace Qurrah.Data.Repository.IRepository
{
    public interface IFAQRepository
    {
        Task AddWithLocaliedPropertiesWithSaveAsync(FAQ faqType, List<LocalizedProperty> localizedProperties);
        Task<ActionResult> UpdateWithLocalizedPropertiesWithSaveAsync(FAQ faq, List<LocalizedProperty> localizedProperties);
        Task<FAQWithLocalizedProperties> SingleOrDefaultWithLocalizedProperties(Expression<Func<FAQ, bool>> filter, string includedProperties = null, bool tracked = true);
        Task<ActionResult> RemoveWithLocalizedPropertiesWithSaveAsync(int id);
        Task<IEnumerable<FAQClassifiedWithLocalizedProperties>> GetAllClassifiedByTypeAsync();
    }
}