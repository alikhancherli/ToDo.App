using Microsoft.AspNetCore.Identity;
using ToDo.App.Domain.Entities;

namespace ToDo.App.Infrastructure.Persistence
{
    public class SeedData : ISeedData
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;

        public SeedData(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            ArgumentNullException.ThrowIfNull(nameof(userManager));
            ArgumentNullException.ThrowIfNull(nameof(roleManager));
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task Seed()
        {
            string roleName = "NormalUser";
            string userName = "TestUser";
            string password = "@Li1357902468";

            if (!await _roleManager.RoleExistsAsync(roleName))
                await _roleManager.CreateAsync(new Role() { Name = roleName });

            if (await UserNotExists(userName))
            {
                var user = User.Create("Ali", "khancherli",userName, "a.khahcherli2017@gmail.com");
                user.PhoneNumber = "09102936612";
                var result = await _userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await _userManager.AddPasswordAsync(user, password);
                    await _userManager.AddToRoleAsync(user, roleName);
                }
            }

        }

        private async Task<bool> UserNotExists(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);

            return user is null;
        }
    }
}
