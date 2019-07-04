using GestorEventos.DAL;
using GestorEventos.Models;
using GestorEventos.WebApi.Auth;
using GestorEventos.WebApi.Helpers;
using GestorEventos.WebApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Net;
using System.Text;
using GestorEventos.BLL;
using GestorEventos.BLL.Interfaces;
using GestorEventos.DAL.Repositories;
using GestorEventos.DAL.Repositories.Interfaces;
using GestorEventos.Models.Entities;
using DinkToPdf;
using DinkToPdf.Contracts;

namespace GestorEventos.WebApi
{
    public class Startup
    {
        private const string SecretKey = "iNivDmHLpUA223sqsfhqGbMRdRj1PVkH"; // todo: get this from somewhere secure
        private readonly SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<IdentityUser>().AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>();

            services.AddSingleton<IJwtFactory, JwtFactory>();
            services.TryAddTransient<IHttpContextAccessor, HttpContextAccessor>();

            //Repository Services
            services.TryAddTransient<IRepository<Event>, Repository<Event>>();
            services.TryAddTransient<IRepository<EventTopic>, Repository<EventTopic>>();
            services.TryAddTransient<IRepository<Participant>, Repository<Participant>>();
            services.TryAddTransient<IRepository<EventSchedule>, Repository<EventSchedule>>();
            services.TryAddTransient<IRepository<Location>, Repository<Location>>();
            services.TryAddTransient<IRepository<Attendant>, Repository<Attendant>>();
            services.TryAddTransient<IRepository<Activity>, Repository<Activity>>();
            services.TryAddTransient<IRepository<ActivityType>, Repository<ActivityType>>();
            services.TryAddTransient<IRepository<Speaker>, Repository<Speaker>>();
            services.TryAddTransient<IRepository<Certificate>, Repository<Certificate>>();
            services.TryAddTransient<IRepository<Country>, Repository<Country>>();
            services.TryAddTransient<IRepository<City>, Repository<City>>();
            //Logic Services
            services.TryAddTransient<IAccreditationLogic, AccreditationLogic>();
            services.TryAddTransient<IActivitiesLogic, ActivitiesLogic>();
            services.TryAddTransient<IAttendantsLogic, AttendantsLogic>();
            services.TryAddTransient<IAuthLogic, AuthLogic>();
            services.TryAddTransient<IEventsLogic, EventsLogic>();
            services.TryAddTransient<IGeographicsLogic, GeographicsLogic>();
            services.TryAddTransient<IFilesLogic, FilesLogic>();
            services.TryAddTransient<ILocationsLogic, LocationsLogic>();
            services.TryAddTransient<ISchedulesLogic, SchedulesLogic>();
            services.TryAddTransient<ISendGridLogic, SendGridLogic>();
            services.TryAddTransient<ISpeakersLogic, SpeakersLogic>();
            services.TryAddTransient<IUsersLogic, UsersLogic>();

            // jwt wire up
            // Get options from app settings
            var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtIssuerOptions));

            // Configure JwtIssuerOptions
            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
                options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
            });

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)],
                    
                ValidateAudience = true,
                ValidAudience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _signingKey,

                RequireExpirationTime = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(configureOptions =>
            {
                configureOptions.ClaimsIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                configureOptions.TokenValidationParameters = tokenValidationParameters;
                configureOptions.SaveToken = true;
            });

            // api user claim policy
            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiUser", policy => policy.RequireClaim(Constants.Strings.JwtClaimIdentifiers.Rol, Constants.Strings.JwtClaims.ApiAccess));
            });

            // add identity
            var builder = services.AddIdentityCore<AppUser>(o =>
            {
                // configure identity options
                o.Password.RequireDigit = false;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 6;
            });
            builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), builder.Services);
            builder.AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSwaggerDocumentation();

            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwaggerDocumentation();
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors(builder =>
                builder.WithOrigins("http://localhost:4200")
                       .AllowAnyHeader().AllowAnyMethod()
            );

            app.UseExceptionHandler(
             builder =>
             {
                 builder.Run(
                            async context =>
                         {
                             context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                             context.Response.Headers.Add("Access-Control-Allow-Origin", "*");

                             var error = context.Features.Get<IExceptionHandlerFeature>();
                             if (error != null)
                             {
                                 await context.Response.WriteAsync(error.Error.Message).ConfigureAwait(false);
                             }
                         });
             });

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseMvc();
        }
    }
}
