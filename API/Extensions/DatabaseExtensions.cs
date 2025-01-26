﻿using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using RestaurantOrdering.Events.Infrastructure.Database;

namespace API.Extensions;

public static class DatabaseExtensions
{
    public static IServiceCollection AddDatabaseServices(this IServiceCollection services, IConfiguration configuration)
    {
        //services.AddDbContext<RestaurantOrderingContext>(opt =>
        //{
        //    opt.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
        //});

        services.AddDbContext<EventsDatabaseContext>(opt =>
        {
            opt.UseSqlite(configuration.GetConnectionString("EventsDatabaseContext"));
        });

        return services;
    }

    public static async Task UseDatabaseMigrationAndSeeding(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var services = scope.ServiceProvider;

        try
        {
            var context = services.GetRequiredService<RestaurantOrderingContext>();
            await context.Database.MigrateAsync();
            await Seed.SeedData(context);
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred during migration");
        }
    }
}
