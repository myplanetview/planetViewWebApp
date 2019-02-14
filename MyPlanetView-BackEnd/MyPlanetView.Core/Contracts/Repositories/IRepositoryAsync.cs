﻿using Microsoft.EntityFrameworkCore.Query;
using MyPlanetView.Core.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyPlanetView.Core.Contracts.Repositories
{
    public interface IRepositoryAsync<T> where T: class
    {
        Task<T> SingleAsync(Expression<Func<T, bool>> predicate = null,
                            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
                            bool disableTracking = true);

        IEnumerable<T> GetAsync(Expression<Func<T, bool>> predicate = null);


        Task<IPaginate<T>> GetListAsync(Expression<Func<T, bool>> predicate = null,
                                        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                        Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
                                        int index = 0,
                                        int size = 20,
                                        bool disableTracking = true,
                                        CancellationToken token = default(CancellationToken));

        Task AddAsync(T entity, CancellationToken cancellationToken = default(CancellationToken));

        Task AddAsync(params T[] entities);

        Task AddAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default(CancellationToken));

        void UpdateAsync(T entity);
    
    }
}
