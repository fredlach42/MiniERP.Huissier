using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MiniERP.Application.Common.Interfaces;
using MiniERP.Infrastructure.Data;
using MiniERP.Infrastructure.Repositories;

namespace MiniERP.Infrastructure.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        // Base de données
        services.AddDbContext<HuissierDbContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));

        // Repositories
        services.AddScoped<IDossierRepository, DossierRepository>();

        return services;
    }
}