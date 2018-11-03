using GestorEventos.Core;
using GestorEventos.DAL;
using GestorEventos.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using GestorEventos.Models.Entities;

namespace GestorEventos.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;

                try
                {
                    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                    var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();

                    AppDbInitializer.Initialize(roleManager, userManager);
                    
                }
                catch (Exception ex)
                {
                }
            }

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
