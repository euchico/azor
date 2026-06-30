namespace Azor.Api;

public class Program
{
    public static void Main(string[] args)
    {
        const string AngularDevCorsPolicy = "AngularDevCorsPolicy";

        var builder = WebApplication.CreateBuilder(args);
        
        builder.Services.AddCors(options =>
        {
            options.AddPolicy(name: AngularDevCorsPolicy, policy =>
            {
                policy
                    .WithOrigins(
                        "http://localhost:4200",
                        "https://localhost:4200"
                    )
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });

        builder.Services.AddControllers();
        builder.Services.AddOpenApi();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        // PENDENTE: Configurar redirecionamento de https.
        // app.UseHttpsRedirection();
        app.UseCors(AngularDevCorsPolicy);
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}

