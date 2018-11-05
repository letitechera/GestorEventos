﻿// <auto-generated />
using System;
using GestorEventos.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GestorEventos.DAL.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GestorEventos.Models.Entities.Activity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ActivityTypeId");

                    b.Property<string>("CreatedById");

                    b.Property<string>("CreatedByName");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Description");

                    b.Property<TimeSpan>("EndTime");

                    b.Property<int>("EventScheduleId");

                    b.Property<string>("ModifiedById");

                    b.Property<string>("ModifiedByName");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("Name");

                    b.Property<TimeSpan>("StartTime");

                    b.HasKey("Id");

                    b.HasIndex("ActivityTypeId");

                    b.HasIndex("EventScheduleId");

                    b.ToTable("Activities");
                });

            modelBuilder.Entity("GestorEventos.Models.Entities.ActivityType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreatedById");

                    b.Property<string>("CreatedByName");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("ModifiedById");

                    b.Property<string>("ModifiedByName");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("ActivityTypes");
                });

            modelBuilder.Entity("GestorEventos.Models.Entities.AppUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTime>("DateOfBirth");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("Enabled");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");

                    b.HasDiscriminator<string>("Discriminator").HasValue("AppUser");
                });

            modelBuilder.Entity("GestorEventos.Models.Entities.Attendant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreatedById");

                    b.Property<string>("CreatedByName");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("ModifiedById");

                    b.Property<string>("ModifiedByName");

                    b.Property<DateTime?>("ModifiedDate");

                    b.HasKey("Id");

                    b.ToTable("Attendants");
                });

            modelBuilder.Entity("GestorEventos.Models.Entities.Certificate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreatedById");

                    b.Property<string>("CreatedByName");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("ModifiedById");

                    b.Property<string>("ModifiedByName");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<int>("ParticipantId");

                    b.Property<bool>("Sent");

                    b.Property<string>("TemplateId");

                    b.HasKey("Id");

                    b.HasIndex("ParticipantId");

                    b.ToTable("Certificates");
                });

            modelBuilder.Entity("GestorEventos.Models.Entities.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool?>("Canceled");

                    b.Property<string>("CreatedById");

                    b.Property<string>("CreatedByName");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Description");

                    b.Property<DateTime>("EndDate");

                    b.Property<int>("EventTopicId");

                    b.Property<string>("Image");

                    b.Property<int>("LocationId");

                    b.Property<string>("ModifiedById");

                    b.Property<string>("ModifiedByName");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("Name");

                    b.Property<int>("OrganizerId");

                    b.Property<string>("OrganizerId1");

                    b.Property<DateTime>("StartDate");

                    b.HasKey("Id");

                    b.HasIndex("EventTopicId");

                    b.HasIndex("LocationId");

                    b.HasIndex("OrganizerId1");

                    b.ToTable("Event");
                });

            modelBuilder.Entity("GestorEventos.Models.Entities.EventSchedule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreatedById");

                    b.Property<string>("CreatedByName");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<DateTime>("Date");

                    b.Property<int>("EventId");

                    b.Property<string>("ModifiedById");

                    b.Property<string>("ModifiedByName");

                    b.Property<DateTime?>("ModifiedDate");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.ToTable("EventSchedule");
                });

            modelBuilder.Entity("GestorEventos.Models.Entities.EventTopic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreatedById");

                    b.Property<string>("CreatedByName");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("ModifiedById");

                    b.Property<string>("ModifiedByName");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("EventTopic");
                });

            modelBuilder.Entity("GestorEventos.Models.Entities.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address1");

                    b.Property<string>("Address2");

                    b.Property<int>("Capacity");

                    b.Property<string>("City");

                    b.Property<string>("Country");

                    b.Property<string>("CreatedById");

                    b.Property<string>("CreatedByName");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<float>("Latitude");

                    b.Property<float>("Longitude");

                    b.Property<string>("ModifiedById");

                    b.Property<string>("ModifiedByName");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Location");
                });

            modelBuilder.Entity("GestorEventos.Models.Entities.Participant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AttendantId");

                    b.Property<string>("CreatedById");

                    b.Property<string>("CreatedByName");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<int>("EventId");

                    b.Property<string>("ModifiedById");

                    b.Property<string>("ModifiedByName");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("QRCode");

                    b.HasKey("Id");

                    b.HasIndex("AttendantId");

                    b.HasIndex("EventId");

                    b.ToTable("Participant");
                });

            modelBuilder.Entity("GestorEventos.Models.Entities.Speaker", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ActivityId");

                    b.Property<string>("Company");

                    b.Property<string>("ContactUrl");

                    b.Property<string>("CreatedById");

                    b.Property<string>("CreatedByName");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Email");

                    b.Property<string>("FisrtName");

                    b.Property<string>("Image");

                    b.Property<string>("LastName");

                    b.Property<string>("ModifiedById");

                    b.Property<string>("ModifiedByName");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("Nationality");

                    b.Property<string>("Position");

                    b.HasKey("Id");

                    b.HasIndex("ActivityId");

                    b.ToTable("Speaker");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("Name")
                        .HasMaxLength(128);

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("GestorEventos.Models.Entities.Organizer", b =>
                {
                    b.HasBaseType("GestorEventos.Models.Entities.AppUser");

                    b.Property<string>("Address");

                    b.Property<string>("Description");

                    b.Property<string>("Image");

                    b.Property<string>("Name");

                    b.Property<int>("OrganizationId");

                    b.Property<string>("Phone");

                    b.ToTable("Organizer");

                    b.HasDiscriminator().HasValue("Organizer");
                });

            modelBuilder.Entity("GestorEventos.Models.Entities.Activity", b =>
                {
                    b.HasOne("GestorEventos.Models.Entities.ActivityType", "ActivityType")
                        .WithMany()
                        .HasForeignKey("ActivityTypeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GestorEventos.Models.Entities.EventSchedule", "EventSchedule")
                        .WithMany("Activities")
                        .HasForeignKey("EventScheduleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GestorEventos.Models.Entities.Certificate", b =>
                {
                    b.HasOne("GestorEventos.Models.Entities.Participant", "Participant")
                        .WithMany()
                        .HasForeignKey("ParticipantId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GestorEventos.Models.Entities.Event", b =>
                {
                    b.HasOne("GestorEventos.Models.Entities.EventTopic", "EventTopic")
                        .WithMany()
                        .HasForeignKey("EventTopicId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GestorEventos.Models.Entities.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GestorEventos.Models.Entities.Organizer", "Organizer")
                        .WithMany("Events")
                        .HasForeignKey("OrganizerId1");
                });

            modelBuilder.Entity("GestorEventos.Models.Entities.EventSchedule", b =>
                {
                    b.HasOne("GestorEventos.Models.Entities.Event", "Event")
                        .WithMany("Schedules")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GestorEventos.Models.Entities.Participant", b =>
                {
                    b.HasOne("GestorEventos.Models.Entities.Attendant", "Attendant")
                        .WithMany()
                        .HasForeignKey("AttendantId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GestorEventos.Models.Entities.Event", "Event")
                        .WithMany("Participants")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GestorEventos.Models.Entities.Speaker", b =>
                {
                    b.HasOne("GestorEventos.Models.Entities.Activity", "Activity")
                        .WithMany("Speakers")
                        .HasForeignKey("ActivityId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("GestorEventos.Models.Entities.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("GestorEventos.Models.Entities.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GestorEventos.Models.Entities.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("GestorEventos.Models.Entities.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
