using Domain.Models.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Domain.Extensions;

public static class ServiceExtension
{
    public static IServiceCollection AddDomain(this IServiceCollection services, IConfiguration configuration)
    {
        services.RegisterOptions(configuration);
        return services;
    }

    private static IServiceCollection RegisterOptions(this IServiceCollection services, IConfiguration configuration)
    {
        return services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.SectionName));
    }
}