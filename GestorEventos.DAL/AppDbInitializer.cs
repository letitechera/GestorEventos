using Microsoft.AspNetCore.Identity;
using GestorEventos.Core;
using GestorEventos.Models;

namespace GestorEventos.DAL
{
    public static class AppDbInitializer
    {
        public static void Initialize(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
        {
            EnsureRoles(roleManager);
            EnsureUsers(userManager);
        }

        private static void EnsureRoles(RoleManager<IdentityRole> roleManager)
        {
            // Admin
            if (!roleManager.RoleExistsAsync(Constants.RoleNameAdmin).Result)
            {
                roleManager.CreateAsync(new IdentityRole(Constants.RoleNameAdmin)).Wait();
            }

            // Organizer
            if (!roleManager.RoleExistsAsync(Constants.RoleNameOrganizer).Result)
            {
                roleManager.CreateAsync(new IdentityRole(Constants.RoleNameOrganizer)).Wait();
            }

            // Creditor
            if (!roleManager.RoleExistsAsync(Constants.RoleNameCreditor).Result)
            {
                roleManager.CreateAsync(new IdentityRole(Constants.RoleNameCreditor)).Wait();
            }

            // Participant
            if (!roleManager.RoleExistsAsync(Constants.RoleNameParticipant).Result)
            {
                roleManager.CreateAsync(new IdentityRole(Constants.RoleNameParticipant)).Wait();
            }
        }

        private static void EnsureUsers(UserManager<AppUser> userManager)
        {
            var admin = new AppUser
            {
                Email = Constants.DefaultAdmin_Email,
                EmailConfirmed = true,
                UserName = Constants.DefaultAdmin_Email,
                FirstName = "Super",
                LastName = "Admin",
                Enabled = true,
            };

            var result = userManager.CreateAsync(admin, "Password01").Result;

            if (result.Succeeded)
            {
                userManager.AddToRoleAsync(admin, Constants.RoleNameAdmin).Wait();
            }
        }
    }
}