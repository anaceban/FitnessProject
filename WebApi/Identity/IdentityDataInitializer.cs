using ApplicationFitness.Domain.Models;
using ApplicationFitness.Domain.Models.Auth;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Identity
{
    public static class IdentityDataInitializer
    {
        public static void SeedData(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            SeedUsers(userManager);
            SeedRoles(roleManager);
        }
        public static void SeedUsers(UserManager<User> userManager)
        {
            if(userManager.FindByNameAsync("user@gmailcom").Result == null)
            {
                var user = new User
                {
                    UserName = "user",
                    Email = "user@gmailcom"
                };
                var result = userManager.CreateAsync(user, "First").Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "user").Wait();
                }
            }
        }
        public static void SeedRoles(RoleManager<Role> roleManager)
        {
            if (!roleManager.RoleExistsAsync("user").Result)
            {
                Role role = new Role("user");
                var result = roleManager.CreateAsync(role).Result;
            }
        }

    }
}
