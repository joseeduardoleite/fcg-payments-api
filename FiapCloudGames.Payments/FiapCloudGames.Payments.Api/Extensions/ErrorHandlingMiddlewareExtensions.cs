using FiapCloudGames.Payments.Api.Middleware;
using System.Diagnostics.CodeAnalysis;

namespace FiapCloudGames.Payments.Api.Extensions;

[ExcludeFromCodeCoverage]
public static class ErrorHandlingMiddlewareExtensions
{
    public static IApplicationBuilder UseErrorHandlingMiddleware(this IApplicationBuilder builder)
        => builder.UseMiddleware<ErrorHandlingMiddleware>();
}