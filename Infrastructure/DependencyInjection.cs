using Application.Abstractions;
using Application.Authentication;
using Application.Interfaces;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var dbPath = Path.Combine(
            AppContext.BaseDirectory,
            "tasks.db");

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite($"Data Source={dbPath}"));

        return services;
    }


    public static IServiceCollection AddServices(this IServiceCollection services) 
    {
        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
        services.AddScoped<ITaskRepository, TaskRepository>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        return services;
    }
}
