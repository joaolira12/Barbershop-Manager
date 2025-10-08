using BarberShopManager.Domain.Repositories;
using BarberShopManager.Domain.Repositories.Clients;
using BarberShopManager.Domain.Repositories.Services;
using BarberShopManager.Infrastructure.DataAccess;
using BarberShopManager.Infrastructure.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BarberShopManager.Infrastructure;
public static class DependencyInjectionExtension
{
    public static void AddDependencyInjection(this IServiceCollection service, IConfiguration configuration)
    {
        AddDbContextService(service, configuration);
        AddDataAccess(service);
    }

    private static void AddDbContextService(IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("Connection");
        var serverVersion = new MySqlServerVersion(new Version(8, 0, 42));

        services.AddDbContext<BarberShopManagerDbContext>(config => config.UseMySql(connectionString, serverVersion));
    }

    private static void AddDataAccess(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IClientWriteRepository, ClientsRepository>();
        services.AddScoped<IClientReadRepository, ClientsRepository>();
        services.AddScoped<IClientDeleteRepository, ClientsRepository>();
        services.AddScoped<IClientUpdateRepository, ClientsRepository>();
        services.AddScoped<IServiceWriteRepository, ServicesRepository>();
        services.AddScoped<IServiceReadRepository, ServicesRepository>();
        services.AddScoped<IServiceUpdateRepository, ServicesRepository>();
        services.AddScoped<IServiceDeleteRepository, ServicesRepository>();
    }
}
