namespace MyPlanetView.Core.Contracts.Repositories
{
    public interface IRepositoryFactory
    {
        IRepository<T> GetRepository<T>() where T : class;
        IRepositoryAsync<T> GetRepositoryAsync<T>() where T : class;
        IReadRepository<T> GetReadOnlyRepository<T>() where T : class;
    }
}
