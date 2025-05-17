
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.Extensions;

public static class ServiceExtension
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration)

    {
        services.AddSqlite<EntityContext>("Data Source=database.db");
        // services.AddDbContext<EntityContext>(options =>
        //     options.UseSqlite(connectionString));

        return services;

    }
}