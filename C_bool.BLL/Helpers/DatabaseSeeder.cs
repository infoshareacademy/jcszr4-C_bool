using System.Threading.Tasks;
using C_bool.BLL.DAL.Entities;
using C_bool.BLL.Repositories;
using C_bool.BLL.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace C_bool.BLL.Helpers
{
    public class DatabaseSeeder
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<UserRole> _roleManager;
        private readonly IReportService _reportService;

        public DatabaseSeeder(UserManager<User> userManager, RoleManager<UserRole> roleManager, IReportService reportService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _reportService = reportService;
        }

        public async Task Seed()
        {
            var user = await this._userManager.Users.FirstOrDefaultAsync();
            if (user == null)
            {
                await _roleManager.CreateAsync(new UserRole { Name = "Admin" });
                await _roleManager.CreateAsync(new UserRole { Name = "User" });
                await _roleManager.CreateAsync(new UserRole { Name = "Moderator" });

                var admin = new User
                {
                    Email = "admin@admin.eu",
                    UserName = "admin",
                    EmailConfirmed = true
                };

                var result = await this._userManager.CreateAsync(admin, "CBoolAllTheWay1@");

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(admin, "Admin");
                    await _reportService.CreateUserReportEntry(admin);
                }

            }
        }
    }
}