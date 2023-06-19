using Qurrah.Data.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Qurrah.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private DbSet<T> _dbSet;
        protected readonly QurrahDbContext DbContext;
        public Repository(QurrahDbContext dbContext)
        {
            DbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }
        public async Task<T> SingleAsync(Expression<Func<T, bool>> filter, string includedProperties = null, bool tracked = true)
        {
            var query = IncludeProperties(includedProperties);
            if (!tracked)
                query = query.AsNoTracking();
            var entity = await query.SingleAsync(filter);
            return entity;
        }
        public async Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> filter, string includedProperties = null, bool tracked = true)
        {
            var query = IncludeProperties(includedProperties);
            if (!tracked)
                query = query.AsNoTracking();
            var entity = await query.SingleOrDefaultAsync(filter);
            return entity;
        }
        public async Task<T> FirstAsync(Expression<Func<T, bool>> filter, string includedProperties = null, bool tracked = true)
        {
            var query = IncludeProperties(includedProperties);
            if (!tracked)
                query = query.AsNoTracking();
            var entity = await query.FirstAsync(filter);
            return entity;
        }
        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> filter, string includedProperties = null, bool tracked = true)
        {
            var query = IncludeProperties(includedProperties);
            if (!tracked)
                query = query.AsNoTracking();
            var entity = await query.FirstOrDefaultAsync(filter);
            return entity;
        }
        public async Task<IEnumerable<T>> GetAllAsync(string includedProperties = null)
        {
            var query = IncludeProperties(includedProperties);
            var entities = await query.ToListAsync();
            return entities;
        }
        public async Task<IEnumerable<T>> WhereAsync(Expression<Func<T, bool>> filter, string includedProperties = null)
        {
            var query = IncludeProperties(includedProperties);
            var entities = await query.Where(filter).ToListAsync();
            return entities;
        }
        public async Task AddAsync(T entity)
        {
            _dbSet.AddAsync(entity);
        }
        public virtual void Update(T entity)
        {
            DbContext.Entry(entity).State = EntityState.Modified;
        }
        public void Remove(T entity)
        {
            _dbSet.Attach(entity);
            DbContext.Entry(entity).State = EntityState.Deleted;
        }
        public void RemoveRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        private IQueryable<T> IncludeProperties(string includedProperties)
        {
            IQueryable<T> query = _dbSet;
            if (!string.IsNullOrWhiteSpace(includedProperties))
            {
                var properties = includedProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                if (properties?.Any() == true)
                    properties.ForEach(prop => query = query.Include(prop));
            }
            return query;
        }
    }
}