using Microsoft.EntityFrameworkCore;
using MyPlanetView.Core.Contracts.Repositories;
using System;

namespace MyPlanetView.Core.Contracts
{
    public interface IUnitOfWork: IDisposable
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
        IRepositoryAsync<TEntity> GetRepositoryAsync<TEntity>() where TEntity : class;
        IReadRepository<TEntity> GetReadOnlyRepository<TEntity>() where TEntity : class;

        int SaveChanges();
    }

    public interface IUnitOfWork<TContext> : IUnitOfWork where TContext: DbContext
    {
        TContext Context { get; }
    }
}
