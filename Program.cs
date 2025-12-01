
using CairoGo.Models.DbContextApp;
using CairoGo.Models.Entity;
using CairoGo.Repository.Implementations;
using CairoGo.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using System.Text.Json.Serialization;

namespace CairoGo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.AddDbContext<CairoGoDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.UseInlineDefinitionsForEnums();
            });
            builder.Services.AddScoped(typeof(IBaseRepo<>), typeof(Repository<>));
            builder.Services.AddScoped<IPlaceRepository,PlaceRepository>();
            builder.Services.AddScoped<ITripDayRepo, TripDayRepo>();
            builder.Services.AddScoped<ITripSlotRepo, TripSlotRepo>();
            builder.Services.AddScoped<IActivityTypeRepo, ActivityTypeRepo>();
            builder.Services.AddScoped<IPreferenceRepo, PreferenceRepo>();
            builder.Services.AddScoped<ITripPlaneRepo, TripPlaneRepo>();
            builder.Services.AddScoped<IExperimentAssignmentRepo, ExperimentAssignmentRepo>();
            builder.Services.AddScoped<IPlaceVibeTagRepo, PlaceVibeTagRepo>();
            builder.Services.AddScoped<IUserPreferenceSignalRepository, UserPreferenceSignalRepository>();
            builder.Services.AddScoped<ISearchSessionRepo, SearchSessionRepo>();
            builder.Services.AddScoped<IPlaceOperatingHoursRepo, PlaceOperatingHoursRepo>();

            builder.Services.AddIdentity<UserApplication, IdentityRole<Guid>>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequiredLength = 8;
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<CairoGoDbContext>()
            .AddDefaultTokenProviders();

            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IJwtTokenRepository, JwtTokenRepository>();

            var jwt = builder.Configuration.GetSection("Jwt");

            builder.Services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = jwt["Issuer"],
                        ValidAudience = jwt["Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(jwt["Key"])
                        )
                    };
                });
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend", policy =>
                {
                    policy.WithOrigins(
                            "https://mega-project-eta.vercel.app/",
                            "http://localhost:3000"
                        )
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors("AllowFrontend");

            app.MapControllers();

            app.Run();
        }
    }
}
