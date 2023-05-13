using FootballScout.Authentication.Model;
using FootballScout.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace FootballScout.Data
{
    public class DatabaseSeeder
    {
        private readonly UserManager<RestUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DatabaseSeeder(UserManager<RestUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task SeedAsync()
        {
            foreach (var role in UserRoles.All)
            {
                var roleExist = await _roleManager.RoleExistsAsync(role);
                if (!roleExist)
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            var newAdminUser = new RestUser
            {
                UserName = "Paulius",
                Email = "eidimtas.paulius@gmail.com"
            };

            var existingAdminUser = await _userManager.FindByNameAsync(newAdminUser.UserName);
            if (existingAdminUser == null)
            {
                var createdAdminUserResult = await _userManager.CreateAsync(newAdminUser, "VerySafePassword123?");
                if (createdAdminUserResult.Succeeded)
                {
                    await _userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }
            }
        }
    }
}
