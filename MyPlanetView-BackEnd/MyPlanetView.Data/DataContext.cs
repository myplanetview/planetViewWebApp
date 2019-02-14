using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyPlanetView.Core.Models;
using System;

namespace MyPlanetView.Data
{
    public class DataContext: IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public DataContext(DbContextOptions<DataContext> options)
         : base(options)
        {
            
        }
    }
}
