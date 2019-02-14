using Microsoft.EntityFrameworkCore;
using MyPlanetView.Core.Contracts;
using MyPlanetView.Core.Contracts.Repositories;
using MyPlanetView.Data.Repositories;
using System;
using System.Collections.Generic;

namespace MyPlanetView.Data
{
    public class UnitOfWork<TContext> : IRepositoryFactory, IUnitOfWork<TContext>, IUnitOfWork where TContext : DbContext, IDisposable
    {
        public TContext Context { get; }
        private Dictionary<Type, object> _repositories;

        public UnitOfWork(TContext context)
        {
            Context = context ?? throw new ArgumentException(nameof(Exception));
        }

        public IReadRepository<T> GetReadOnlyRepository<T>() where T : class
        {
            if (_repositories == null) _repositories = new Dictionary<Type, object>();
            var type = typeof(T);
            if (!_repositories.ContainsKey(type)) _repositories[type] = new RepositoryReadOnly<T>(Context);
            return (IReadRepository<T>)_repositories[type];
        }

        public IRepository<T> GetRepository<T>() where T : class
        {
            if (_repositories == null) _repositories = new Dictionary<Type, object>();

            var type = typeof(T);
            if (!_repositories.ContainsKey(type)) _repositories[type] = new Repository<T>(Context);
            return (IRepository<T>)_repositories[type];
        }

        public IRepositoryAsync<T> GetRepositoryAsync<T>() where T : class
        {
            if (_repositories == null) _repositories = new Dictionary<Type, object>();

            var type = typeof(T);
            if (!_repositories.ContainsKey(type)) _repositories[type] = new RepositoryAsync<T>(Context);
            return (IRepositoryAsync<T>)_repositories[type];

        }

        public int SaveChanges()
        {
            return Context.SaveChanges();
        }

        public void Dispose()
        {
            Context?.Dispose();
        }
    }
}
