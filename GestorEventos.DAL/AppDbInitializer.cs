using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GestorEventos.Core;
using GestorEventos.Models;
using System.Threading.Tasks;

namespace GestorEventos.DAL
{
    public class AppDbInitializer
    {
        private AppDbContext _context;

        public AppDbInitializer(AppDbContext context)
        {
            _context = context;
        }

        public void Initialize()
        {
            EnsureRoles();
            EnsureSuperAdmin();
        }

        private void EnsureRoles()
        {
            var roleStore = new RoleStore<IdentityRole>(_context);

            // Admin
            if (!roleStore.Roles.Any(r => r.Name.Equals(Constants.RoleNameAdmin)))
            {
                roleStore.CreateAsync(new IdentityRole(Constants.RoleNameAdmin));
            }
        }

        private void EnsureSuperAdmin()
        {
            var superAdmin = new AppUser
            {
                Email = Constants.DefaultAdmin_Email,
                EmailConfirmed = true,
                UserName = Constants.DefaultAdmin_Email,
                FirstName = "Super",
                LastName = "Admin",
                Enabled = true
            };

            if (!_context.Users.Any(u => u.Email.Equals(superAdmin.Email)))
            {
                var password = new PasswordHasher<AppUser>();
                var hashed = password.HashPassword(superAdmin, Constants.DefaultAdmin_Password);
                superAdmin.PasswordHash = hashed;
                var userStore = new UserStore<AppUser>(_context);
                userStore.CreateAsync(superAdmin);
                userStore.AddToRoleAsync(superAdmin, Constants.RoleNameAdmin);
            }

            _context.SaveChangesAsync();
        }
    }
}