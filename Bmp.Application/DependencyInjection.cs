using Microsoft.Extensions.DependencyInjection;
using Bmp.Application.UseCase;

namespace Bmp.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Use Cases
        services.AddScoped<CreateUserUseCase>();
        services.AddScoped<AuthTokenUseCase>();

        return services;
    }
}
