using LabirentFethiye.Domain.Entities.GlobalEntities.IdentityEntities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace LabirentFethiye.Persistence.SeedDatas
{
    public class DatabaseInitilaizer
    {
        public static async void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var userManager = serviceScope.ServiceProvider.GetService<UserManager<AppUser>>();
                var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<AppRole>>();

                if (userManager.Users.Any() && roleManager.Roles.Any()) { return; }

                if (!await roleManager.RoleExistsAsync("SuperAdmin"))
                {
                    var role = new AppRole { Name = "SuperAdmin" };
                    await roleManager.CreateAsync(role);
                    await roleManager.CreateAsync(new AppRole { Name = "Admin" });
                }

                if (await userManager.FindByNameAsync("info@grandedigital.com") == null)
                {
                    var user = new AppUser
                    {
                        Name = "Grande Digital",
                        SurName = "Grande Digital",
                        Language = "tr",
                        UserName = "info@grandedigital.com",
                        Email = "info@grandedigital.com"
                    };
                    var result = await userManager.CreateAsync(user, "Gd336699**");
                    if (result.Succeeded) { await userManager.AddToRoleAsync(user, "SuperAdmin"); }
                }

            }
        }
    }
}
