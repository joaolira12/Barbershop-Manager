using AutoMapper;
using BarberShopManager.Communication.Clients.Request;
using BarberShopManager.Communication.Clients.Response;
using BarberShopManager.Communication.Services.Request;
using BarberShopManager.Communication.Services.Response;
using BarberShopManager.Domain.Entities;

namespace BarberShopManager.Application.AutoMapper;
public class AutoMapping : Profile
{
    public AutoMapping()
    {
        RequestClientToEntityOrResponse();
        EntityToResponse();
        RequestServiceToEntityOrResponse();
        EntityToService();
    }

    private void RequestClientToEntityOrResponse()
    {
        CreateMap<RequestClientJson, Client>();
        CreateMap<RequestClientJson, ResponseShortClientJson>();
    }

    private void EntityToResponse()
    {
        CreateMap<Client, ResponseShortClientJson>();
        CreateMap<Client, ResponseClientJson>();
    }

    private void RequestServiceToEntityOrResponse()
    {
        CreateMap<RequestServiceJson, Service>();
        CreateMap<RequestServiceJson, ResponseShortServiceJson>();
    }

    private void EntityToService()
    {
        CreateMap<Service, ResponseShortServiceJson>();
        CreateMap<Service, ResponseServiceJson>();
    }
}
