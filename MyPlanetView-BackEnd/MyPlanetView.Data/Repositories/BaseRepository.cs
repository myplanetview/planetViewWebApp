using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using MyPlanetView.Core.Contracts.Repositories;
using MyPlanetView.Core.Paging;

namespace MyPlanetView.Data.Repositories
{
    public class BaseRepository<T>: IReadRepository<T> where T:class
    {
        protected readonly DbContext _dbContext;
        protected readonly DbSet<T> _dbSet;

        public BaseRepository(DbContext context)
        {
            _dbContext = context ?? throw new ArgumentException(nameof(Exception));
            _dbSet = _dbContext.Set<T>();
        }

        public IPaginate<T> GetList(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, int index = 0, int size = 20, bool disableTracking = true)
        {
            IQueryable<T> query = _dbSet;
            if (disableTracking) query = query.AsNoTracking();

            if (include != null) query = include(query);

            if (predicate != null) query = query.Where(predicate);

            return orderBy != null ? orderBy(query).ToPaginate(index, size) : query.ToPaginate(index, size);
        }

        public IPaginate<TResult> GetList<TResult>(Expression<Func<T, TResult>> selector, Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, int index = 0, int size = 20, bool disableTracking = true) where TResult : class
        {
            IQueryable<T> query = _dbSet;
            if (disableTracking) query = query.AsNoTracking();

            if (include != null) query = include(query);

            if (predicate != null) query = query.Where(predicate);

            return orderBy != null
                ? orderBy(query).Select(selector).ToPaginate(index, size)
                : query.Select(selector).ToPaginate(index, size);
        }

        public IQueryable<T> Query(string sql, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        //public virtual IQueryable<T> Query(string sql, params object[] parameters)
        //{
        //    return _dbSet.SqlQuery(sql, parameters);
        //}

        public T Search(params object[] keyValues) => _dbSet.Find(keyValues);

        public T Single(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, int index = 0, int size = 20, bool disableTracking = true)
        {
            IQueryable<T> query = _dbSet;
            if (disableTracking) query = query.AsNoTracking();

            include?.Invoke(query);

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null)
                return orderBy(query).FirstOrDefault();

            return query.FirstOrDefault();
        }
    }
}
