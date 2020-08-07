using Domain;
using HangFire.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Persistence;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Hangfire
{
    public static class AppAuthExtentions
    {
        public static async Task CreateAdminRoleForDefaultUser(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();
            RoleManager<IdentityRole> RoleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            UserManager<IdentityUser> UserManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

            HangFireApplicationDbContext content = scope.ServiceProvider.GetRequiredService<HangFireApplicationDbContext>();

            IdentityResult roleResult;

            //Adding Admin Role
            bool roleCheck = await RoleManager.RoleExistsAsync("Admin");
            if (!roleCheck)
            {
                //create the roles and seed them to the database
                roleResult = await RoleManager.CreateAsync(new IdentityRole("Admin"));
            }

            //Assign Admin role to the main User here we have given our newly registered 
            //login id for Admin management
            IdentityUser user = await UserManager.FindByEmailAsync("avaneesab5@gmail.com");
            if (user != null)
            {
                IList<string> currentUserRoles = await UserManager.GetRolesAsync(user);
                if (!currentUserRoles.Contains("Admin"))
                {
                    await UserManager.AddToRoleAsync(user, "Admin");
                }

            }
        }
    }
}
