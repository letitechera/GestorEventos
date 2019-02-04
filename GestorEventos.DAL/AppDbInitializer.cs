using System;
using Microsoft.AspNetCore.Identity;
using GestorEventos.Core;
using GestorEventos.Models;
using GestorEventos.Models.Entities;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using GestorEventos.DAL.Repositories.Interfaces;
using System.Linq;

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

        public static void Seed(AppDbContext context)
        {
            InsertGeographics(context);
            InsertActivityTypes(context);
            context.Dispose();
        }

        private static void InsertGeographics(AppDbContext context)
        {
            //Remove Json Ignore from cities list in Country for this init to work, add it again for Api calls to work.
            List<Country> items = new List<Country>();
            using (StreamReader r = new StreamReader("DataFiles/countries.json"))
            {
                string json = r.ReadToEnd();
                items = JsonConvert.DeserializeObject<List<Country>>(json);
            }
            if (items.Count > 0)
            {
                foreach (Country cn in items)
                {
                    var existant = context.Countries.Where(e => e.Name == cn.Name).FirstOrDefault();
                    var id = 0;
                    if (existant == null)
                    {
                        var e = context.Countries.Add(cn);
                        context.SaveChanges();
                        id = e.Entity.Id;
                    }
                    else
                    {
                        id = existant.Id;
                    }

                    if (cn.Cities != null && cn.Cities.Count > 0)
                    {
                        foreach (City ci in cn.Cities)
                        {
                            var existantCity = context.Cities.Where(e => e.Name == ci.Name).FirstOrDefault();

                            if (existantCity == null)
                            {
                                ci.CountryId = id;
                                context.Cities.Add(ci);
                                context.SaveChanges();
                            }
                        }
                    }
                }
            }
        }

        private static void InsertActivityTypes(AppDbContext context)
        {
            //Remove Json Ignore from cities list in Country for this init to work, add it again for Api calls to work.
            List<ActivityType> items = new List<ActivityType>();
            using (StreamReader r = new StreamReader("DataFiles/activitytypes.json"))
            {
                string json = r.ReadToEnd();
                items = JsonConvert.DeserializeObject<List<ActivityType>>(json);
            }
            if (items.Count > 0)
            {
                foreach (ActivityType a in items)
                {
                    var existant = context.ActivityTypes.Where(e => e.Name == a.Name).FirstOrDefault();
                    var id = 0;
                    if (existant == null)
                    {
                        var e = context.ActivityTypes.Add(a);
                        context.SaveChanges();
                        id = e.Entity.Id;
                    }
                    else
                    {
                        id = existant.Id;
                    }
                }
            }
        }
    }
}