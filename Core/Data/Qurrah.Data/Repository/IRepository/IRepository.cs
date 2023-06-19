using System.Linq.Expressions;

namespace Qurrah.Data.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task<T> SingleAsync(Expression<Func<T, bool>> filter, string includedProperties = null, bool tracked = false);
        Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> filter, string includedProperties = null, bool tracked = false);
        Task<T> FirstAsync(Expression<Func<T, bool>> filter, string includedProperties = null, bool tracked = false);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> filter, string includedProperties = null, bool tracked = false);
        Task<IEnumerable<T>> GetAllAsync(string includedProperties = null);
        Task<IEnumerable<T>> WhereAsync(Expression<Func<T, bool>> filter, string includedProperties = null);
        Task AddAsync(T entity);
        void Update(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }
}