using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using MyPlanetView.Core.Contracts.Repositories;
using MyPlanetView.Core.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyPlanetView.Data.Repositories
{
    public class RepositoryAsync<T> : IRepositoryAsync<T> where T : class
    {
        protected readonly DbContext _dbContext;
        protected readonly DbSet<T> _dbSet;

        public RepositoryAsync(DbContext context)
        {
            _dbContext = context;
            _dbSet = _dbContext.Set<T>();
        }

        public Task AddAsync(T entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            return _dbSet.AddAsync(entity, cancellationToken);
        }

        public Task AddAsync(params T[] entities)
        {
            return _dbSet.AddRangeAsync(entities);
        }

        public Task AddAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default(CancellationToken))
        {
            return _dbSet.AddRangeAsync(entities);
        }

        public IEnumerable<T> GetAsync(Expression<Func<T, bool>> predicate = null)
        {
            throw new NotImplementedException();
        }

        public Task<IPaginate<T>> GetListAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, int index = 0, int size = 20, bool disableTracking = true, CancellationToken token = default(CancellationToken))
        {
            IQueryable<T> query = _dbSet;
            if (disableTracking) query = query.AsNoTracking();

            if (include != null) query = include(query);

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null)
                return orderBy(query).ToPaginateAsync(index, size, 0, token);
            return query.ToPaginateAsync(index, size, 0, token);
        }

        public async Task<T> SingleAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool disableTracking = true)
        {
            IQueryable<T> query = _dbSet;
            if (disableTracking) query = query.AsNoTracking();

            if (include != null) query = include(query);

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null)
                return await orderBy(query).FirstOrDefaultAsync();

            return await query.FirstOrDefaultAsync();
        }

        public void UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
        }
    }
}
