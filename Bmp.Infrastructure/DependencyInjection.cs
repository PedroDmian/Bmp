using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

using Bmp.Infrastructure.Persistence;
using Bmp.Infrastructure.Repositories;
using Bmp.Infrastructure.Jwt;
using Bmp.Application.Interfaces;
using Bmp.Domain.Repositories;

namespace Bmp.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Database
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        // Repositories
        services.AddScoped<IUserRepository, UserRepository>();

        // Services
        services.AddScoped<IJwtTokenService, JwtTokenService>();

        return services;
    }
}
