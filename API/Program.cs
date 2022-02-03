using Application;
using Infrastructure;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using Application.Authentication;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using API.Middleware;
using NLog.Web;

namespace API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Logging.ClearProviders();
        builder.Logging.SetMinimumLevel(LogLevel.Trace);
        builder.Host.UseNLog();

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<CinemaContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("CinemaConnectionString")));
        builder.Services.AddServices()
            .AddAutoMapper()
            .AddValidators()
            .AddRepositories()
            .AddHelpers()
            .AddHttpContextAccessor()
            .AddScoped<ErrorHandlingMiddleware>()
            .AddPagination()
            .AddScoped<ILogger<ErrorHandlingMiddleware>, Logger<ErrorHandlingMiddleware>>();

        var authenticationSettings = new AuthenticationSettings();
        builder.Configuration.GetSection("Authentication").Bind(authenticationSettings);
        builder.Services.AddSingleton(authenticationSettings);
        builder.Services.AddAuthentication("Bearer")
        .AddJwtBearer(cfg =>
        {
            cfg.RequireHttpsMetadata = false;
            cfg.SaveToken = true;
            cfg.TokenValidationParameters = new()
            {
                ValidIssuer = authenticationSettings.JwtIssuer,
                ValidAudience = authenticationSettings.JwtIssuer,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey))
            };
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseMiddleware<ErrorHandlingMiddleware>();
        app.UseAuthentication();
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}

