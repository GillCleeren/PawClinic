using Microsoft.AspNetCore.Identity;
using PawClinic.Identity.Models;

namespace PawClinic.Identity.Seed
{
    public static class CreateFirstUser
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager)
        {
            var adminUser = new ApplicationUser
            {
                FirstName = "Clinic",
                LastName = "Admin",
                UserName = "admin@pawclinic.com",
                Email = "admin@pawclinic.com",
                EmailConfirmed = true
            };

            var user = await userManager.FindByEmailAsync(adminUser.Email);
            if (user == null)
            {
                await userManager.CreateAsync(adminUser, "Admin123!");
            }
        }
    }
}
