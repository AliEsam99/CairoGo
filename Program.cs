
using CairoGo.Models.DbContextApp;
using CairoGo.Repository.Implementations;
using CairoGo.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
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


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
