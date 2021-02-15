using Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence;
using Presentation.HangFire;
using Presentation.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Presentation
{
    public static class StartupExtentions
    {
        public static async Task UseStartupMethods(this IApplicationBuilder app)
        {
            //IServiceScope scope = await CreateRoles(app);
            HangFireJobScheduler.ScheduleRecurringJobs();
        }

        private static async Task<IServiceScope> CreateRoles(IApplicationBuilder app)
        {
            IServiceScope scope = app.ApplicationServices.CreateScope();
            RoleManager<IdentityRole> RoleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            UserManager<ApplicationUser> UserManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            ApplicationDbContext content = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

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
            ApplicationUser user = await UserManager.FindByEmailAsync("avaneesab5@gmail.com");
            if (user != null)
            {
                IList<string> currentUserRoles = await UserManager.GetRolesAsync(user);
                if (!currentUserRoles.Contains("Admin"))
                {
                    await UserManager.AddToRoleAsync(user, "Admin");
                }

            }

           
            UserSetting currentUserSetting = await content.UserSettings.Where(x => x.ApplicationUserId.Equals(user.Id)).FirstOrDefaultAsync();

            if (currentUserSetting == null)
            {
                content.UserSettings.Add(new UserSetting()
                {
                    ApplicationUserId = user.Id,
                    CurrentTimeZone = "Eastern Standard Time"
                });
                content.SaveChanges();
            }

            return scope;
        }
    }
}
