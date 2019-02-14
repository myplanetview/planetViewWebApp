using Microsoft.AspNetCore.Identity;
using System;

namespace MyPlanetView.Core.Models
{
    public class DefaultRole : IdentityRole<Guid>
    {
        public DefaultRole() : base()
        {

        }

        public DefaultRole(string roleName)
        {
            Name = roleName;
        }
    }
}
