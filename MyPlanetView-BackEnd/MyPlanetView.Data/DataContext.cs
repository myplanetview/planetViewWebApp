using Microsoft.EntityFrameworkCore;

namespace MyPlanetView.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
         : base(options)
        {
            
        }
    }
}
