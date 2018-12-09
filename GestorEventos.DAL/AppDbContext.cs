using GestorEventos.Models;
using GestorEventos.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Claims;

namespace GestorEventos.DAL
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }

        public DbSet<Activity> Activities { get; set; }
        public DbSet<ActivityType> ActivityTypes { get; set; }
        public DbSet<Attendant> Attendants { get; set; }
        public DbSet<Certificate> Certificates { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventSchedule> EventSchedules { get; set; }
        public DbSet<EventTopic> EventTopics { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<Speaker> Speakers { get; set; }

        public override int SaveChanges()
        {
            AddTimestamps();
            return base.SaveChanges();
        }

        private void AddTimestamps()
        {
            var entities = ChangeTracker.Entries().Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((BaseEntity)entity.Entity).CreatedDate = DateTime.UtcNow;
                    //((BaseEntity)entity.Entity).UserCreated = userName;
                    //((BaseEntity)entity.Entity).UserCreatedId = userId;
                }

                ((BaseEntity)entity.Entity).ModifiedDate = DateTime.UtcNow;
                //((BaseEntity)entity.Entity).ModifiedByName = currentUsername;
                //((BaseEntity)entity.Entity).ModifiedById = userId;
            }
        }

    }
}
