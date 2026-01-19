using FiapCloudGames.Payments.Api.Middleware;
using System.Diagnostics.CodeAnalysis;

namespace FiapCloudGames.Payments.Api.Extensions;

[ExcludeFromCodeCoverage]
public static class RequestLoggingMiddlewareExtensions
{
    public static IApplicationBuilder UseRequestLogging(this IApplicationBuilder builder)
        => builder.UseMiddleware<RequestLoggingMiddleware>();
}