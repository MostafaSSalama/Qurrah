﻿using System.Linq.Expressions;

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
        Task<bool> AnyAsync(Expression<Func<T, bool>> filter);
        Task<int> CountAsync(Expression<Func<T, bool>> filter);
        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        void Update(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }
}