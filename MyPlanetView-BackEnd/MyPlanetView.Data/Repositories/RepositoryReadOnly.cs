using Microsoft.EntityFrameworkCore;
using MyPlanetView.Core.Contracts.Repositories;

namespace MyPlanetView.Data.Repositories
{
    public class RepositoryReadOnly<T> : BaseRepository<T> , IReadRepository<T> where T: class
    {
        public RepositoryReadOnly(DbContext context) : base(context)
        {

        }
    }
}
