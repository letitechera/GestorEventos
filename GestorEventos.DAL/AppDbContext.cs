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
        public DbSet<Event> Event { get; set; }
        public DbSet<EventSchedule> EventSchedule { get; set; }
        public DbSet<EventTopic> EventTopic { get; set; }
        public DbSet<Location> Location { get; set; }
        public DbSet<Organizer> Organizer { get; set; }
        public DbSet<Participant> Participant { get; set; }
        public DbSet<Speaker> Speaker { get; set; }

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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Participant>()
                .HasOne(x => x.Certificate)
                .WithOne(x => x.Participant)
                .HasForeignKey<Certificate>(x => x.ParticipantId).IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
