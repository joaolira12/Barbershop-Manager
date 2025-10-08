using BarberShopManager.Application.AutoMapper;
using BarberShopManager.Application.UseCases.Clients.Delete;
using BarberShopManager.Application.UseCases.Clients.GetAll;
using BarberShopManager.Application.UseCases.Clients.GetById;
using BarberShopManager.Application.UseCases.Clients.Register;
using BarberShopManager.Application.UseCases.Clients.Update;
using BarberShopManager.Application.UseCases.Services.Delete;
using BarberShopManager.Application.UseCases.Services.Register;
using BarberShopManager.Application.UseCases.Services.Update;
using Microsoft.Extensions.DependencyInjection;

namespace BarberShopManager.Application;
public static class DependencyInjectionExtension
{
    public static void AddApplication(this IServiceCollection service)
    {
        AddUseCases(service);
        AddAutoMapper(service);
    }

    private static void AddUseCases(IServiceCollection services)
    {
        services.AddScoped<IRegisterClientUseCase, RegisterClientUseCase>();
        services.AddScoped<IGetAllClientsUseCase, GetAllClientsUseCase>();
        services.AddScoped<IGetClientByIdUseCase, GetClientByIdUseCase>();
        services.AddScoped<IDeleteClientUseCase, DeleteClientUseCase>();
        services.AddScoped<IUpdateClientUseCase, UpdateClientUseCase>();
        services.AddScoped<IRegisterServiceUseCase, RegisterServiceUseCase>();
        services.AddScoped<IDeleteServiceUseCase, DeleteServiceUseCase>();
        services.AddScoped<IUpdateServiceUseCase, UpdateServiceUseCase>();
    }

    private static void AddAutoMapper(IServiceCollection services)
    {
        services.AddAutoMapper(cfg => { }, typeof(AutoMapping));
    }
}
