using System.Diagnostics.CodeAnalysis;

namespace FiapCloudGames.Payments.Api;

[ExcludeFromCodeCoverage]
public static class ApiDependencyInjection
{
    public static IServiceCollection AddApiModule(this IServiceCollection services)
    {
        //services.AddScoped<I>

        return services;
    }
}