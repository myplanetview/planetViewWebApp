﻿using Microsoft.EntityFrameworkCore.Query;
using MyPlanetView.Core.Paging;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace MyPlanetView.Core.Contracts.Repositories
{
    public interface IReadRepository<T> where T : class
    {
        IQueryable<T> Query(string sql, params object[] parameters);

        T Search(params object[] keyValues);

        T Single(
                 Expression<Func<T, bool>> predicate = null,
                 Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                 Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
                 int index = 0,
                 int size = 20,
                 bool disableTracking = true
               );

        IPaginate<T> GetList(
                             Expression<Func<T, bool>> predicate = null,
                             Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                             Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
                             int index = 0,
                             int size = 20,
                             bool disableTracking = true
                            );

        IPaginate<TResult> GetList<TResult>(Expression<Func<T, TResult>> selector,
         Expression<Func<T, bool>> predicate = null,
         Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
         Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
         int index = 0,
         int size = 20,
         bool disableTracking = true) where TResult : class;




    }
}
