using Microsoft.Extensions.DependencyInjection;
using TruyenHayPro.Application.Handler;

namespace TruyenHayPro.Application.DI;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<CategoryAdminListHandler>();
        return services;
    }
}