using Infrastructure.Authentication;
using Infrastructure.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Web;

public class Program
{
    public static void Main(string[] args)
    {
        const string AngularDevCorsPolicy = "AngularDevCorsPolicy";

        var builder = WebApplication.CreateBuilder(args);
        var jwtSettings = builder.Configuration.GetSection(JwtSettings.SectionName).Get<JwtSettings>()
            ?? throw new InvalidOperationException("A configuração JWT é obrigatória.");

        if (string.IsNullOrWhiteSpace(jwtSettings.Key))
        {
            throw new InvalidOperationException("A chave JWT deve ser configurada por User Secrets ou variável de ambiente.");
        }

        builder.Services.AddCors(options =>
        {
            options.AddPolicy(name: AngularDevCorsPolicy, policy =>
            {
                policy
                    .WithOrigins(
                        "http://localhost:4200",
                        "https://localhost:4200",
                        "https://projeto-azor.vercel.app"
                    )
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });

        builder.Services.AddControllers();
        builder.Services.AddOpenApi();
        builder.Services.AddInfrastructure(builder.Configuration);
        builder.Services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.MapInboundClaims = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidateAudience = true,
                    ValidAudience = jwtSettings.Audience,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key)),
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
        builder.Services.AddAuthorization();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseCors(AngularDevCorsPolicy);
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}
