using BarberShopManager.Communication.Clients.Request;
using BarberShopManager.Communication.Clients.Response;

namespace BarberShopManager.Application.UseCases.Clients.Register;
public interface IRegisterClientUseCase
{
    public Task<ResponseShortClientJson> Execute(RequestClientJson request);
}
