using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace QuizTopics.Common.Database
{
    public static class DatabaseStartUpExtensions
    {
        // TODO: use it
        public static IHost MigrateDatabase<TContext>(this IHost host) 
            where TContext : DbContext
        {
            if (host == null)
            {
                throw new ArgumentNullException(nameof(host));
            }

            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            services.MigrateDbContext<TContext>();

            return host;
        }

        private static void MigrateDbContext<TContext>(this IServiceProvider services) 
            where TContext : DbContext
        {
            var logger = services.GetRequiredService<ILogger<TContext>>();

            using var context = services.GetRequiredService<TContext>();
            try
            {
                var migrationsNeeded = context.Database.GetPendingMigrations().Any();
                if (!migrationsNeeded)
                {
                    return;
                }

                logger.LogInformation($"Migrating the database: {typeof(TContext)}");
                context.Database.Migrate();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while migrating the database.");
                throw;
            }
        }
    }
}