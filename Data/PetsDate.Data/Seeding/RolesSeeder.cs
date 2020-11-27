namespace PetsDate.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using PetsDate.Common;
    using PetsDate.Data.Models;

    internal class RolesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

            await SeedUserAsync(userManager, GlobalConstants.AdministratorRoleName, GlobalConstants.AdministratorRoleEmail, GlobalConstants.AdministratorRolePassword);

            await SeedRoleAsync(roleManager, GlobalConstants.AdministratorRoleName);

            await SeedUserToRoleAsync(userManager, roleManager, dbContext, GlobalConstants.AdministratorRoleName);
        }

        private static async Task SeedUserToRoleAsync(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, ApplicationDbContext dbContext, string name)
        {
            var user = await userManager.FindByNameAsync(name);

            var role = await roleManager.FindByNameAsync(name);

            var exist = dbContext
                .UserRoles
                .Any(x => x.UserId == user.Id && x.RoleId == role.Id);

            if (exist)
            {
                return;
            }

            await dbContext.UserRoles.AddAsync(new IdentityUserRole<string>
            {
                UserId = user.Id,
                RoleId = role.Id,
            });
        }

        private static async Task SeedUserAsync(UserManager<ApplicationUser> userManager, string userName, string email, string password)
        {
            var user = await userManager.FindByNameAsync(userName);

            if (user == null)
            {
                var result = await userManager.CreateAsync(
                new ApplicationUser
                {
                    UserName = userName,
                    Email = email,
                    EmailConfirmed = true,
                }, password);

                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }
            }
        }

        private static async Task SeedRoleAsync(RoleManager<ApplicationRole> roleManager, string roleName)
        {
            var role = await roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                var result = await roleManager.CreateAsync(new ApplicationRole(roleName));
                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }
            }
        }
    }
}
