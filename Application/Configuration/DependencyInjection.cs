using Microsoft.Extensions.DependencyInjection;
using MiniERP.Application.Common.Interfaces;
using MiniERP.Application.UseCases.Dossiers;

namespace MiniERP.Application.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Services Application
        services.AddScoped<IDossierService, DossierService>();

        return services;
    }
}