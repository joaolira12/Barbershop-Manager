using BarberShopManager.Communication.Clients.Request;

namespace BarberShopManager.Application.UseCases.Clients.Update;
public interface IUpdateClientUseCase
{
    public Task Execute(int id, RequestClientJson request);
}
