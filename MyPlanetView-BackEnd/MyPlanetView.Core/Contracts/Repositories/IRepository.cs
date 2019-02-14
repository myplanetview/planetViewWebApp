using System;
using System.Collections.Generic;

namespace MyPlanetView.Core.Contracts.Repositories
{
    public interface IRepository<T> : IReadRepository<T>, IDisposable where T: class
    {
        void Add(T entity);
        void Add(params T[] entities);
        void Add(IEnumerable<T> entities);

        void Delete(T Entity);
        void Delete(object id);
        void Delete(params T[] entities);
        void Delete(IEnumerable<T> entities);


        void Update(T entity);
        void Update(params T[] entities);
        void Update(IEnumerable<T> entities);
    }
}
