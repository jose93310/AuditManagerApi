using JS.AuditManager.Application.Config;
using JS.AuditManager.Application.IService;
using JS.AuditManager.Application.Service;
using JS.AuditManager.Domain.IRepository;
using JS.AuditManager.Infrastructure.Auth;
using JS.AuditManager.Infrastructure.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Asp.Versioning;
using JS.AuditManager.Infrastructure.Audit;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Controllers
        builder.Services.AddControllers();

        // JWT configuration
        builder.Services.Configure<JwtOptions>(
            builder.Configuration.GetSection("Jwt"));

        // DbContext
        builder.Services.AddDbContext<AuditContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        // Authentication with JWT
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                var jwtOptions = builder.Configuration.GetSection("Jwt").Get<JwtOptions>();
                if (jwtOptions == null)
                    throw new InvalidOperationException("JWT configuration section 'Jwt' is missing or invalid.");

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtOptions.Issuer,
                    ValidAudience = jwtOptions.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtOptions.Key))
                };
            });

        #region Dependency Injection

        // Servicios de aplicación
        builder.Services.AddScoped<IAuthService, AuthService>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<ITokenGeneratorService, TokenGeneratorService>();
        builder.Services.AddScoped<IAuditService, AuditService>();

        // Repositorios de dominio
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<ITokenRepository, TokenRepository>();
        builder.Services.AddScoped<IAuditRepository, AuditRepository>();
        #endregion

        // Swagger/OpenAPI
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();


        builder.Services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
        });

        var app = builder.Build();

        // CORS
        app.UseCors("AllowAll");

        // HTTPS
        app.UseHttpsRedirection();

        // Routing
        app.UseRouting();

        // Swagger (activado en todos los entornos)
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Audit Manager API v1 - By Jose Sanabria");
            c.RoutePrefix = "swagger";
        });

        // Auth
        app.UseAuthentication();
        app.UseAuthorization();

        // Map Controllers
        app.MapControllers();

        app.Run();

    }
}
