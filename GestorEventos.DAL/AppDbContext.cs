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
        private IHttpContextAccessor _httpContextAccessor;

        public AppDbContext(DbContextOptions<AppDbContext> options, IHttpContextAccessor httpContextAccessor)
            : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
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
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }

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
                var userNameClaim = (_httpContextAccessor.HttpContext?.User?.Identity as ClaimsIdentity)?.Claims?.First();

                var userIdClaim = (_httpContextAccessor.HttpContext?.User?.Identity as ClaimsIdentity)?.Claims?.FirstOrDefault(c => c.Type == "id")?.Value;

                var currentUsername = !string.IsNullOrEmpty(userNameClaim?.Value)
                    ? userNameClaim.Value
                    : "Anonymous";

                if (entity.State == EntityState.Added)
                {
                    ((BaseEntity)entity.Entity).CreatedDate = DateTime.Now;
                    ((BaseEntity)entity.Entity).CreatedByName = currentUsername;
                    ((BaseEntity)entity.Entity).CreatedById = userIdClaim;
                }

                ((BaseEntity)entity.Entity).ModifiedDate = DateTime.Now;
                ((BaseEntity)entity.Entity).ModifiedByName = currentUsername;
                ((BaseEntity)entity.Entity).ModifiedById = userIdClaim;
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AppUser>()
            .Property(f => f.DateOfBirth)
            .HasColumnType("datetime");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        //add-migration NameMigration -Project GestorEventos.DAL
        //update-database
    }
}
