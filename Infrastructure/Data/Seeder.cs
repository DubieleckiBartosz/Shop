using Application.Constants;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class Seeder
    {
        public static async Task Seed(RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(Authorization.Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Authorization.Roles.Manager.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Authorization.Roles.User.ToString()));
        }
    }
}
