using Application.Authentication;
using Infrastructure.Authentication;
using Infrastructure.Identity;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("AzorDb")
            ?? throw new InvalidOperationException("A connection string 'AzorDb' é obrigatória.");

        services.AddDbContext<AzorDbContext>(options => options.UseSqlServer(connectionString));
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
        services
            .AddIdentityCore<ApplicationUser>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = false;
            })
            .AddUserStore<AzorUserStore>();

        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddScoped<IAuthenticationService, IdentityAuthenticationService>();
        services.AddScoped<RegisterUserUseCase>();
        services.AddScoped<LoginUseCase>();
        services.AddScoped<GetCurrentUserUseCase>();

        return services;
    }
}
