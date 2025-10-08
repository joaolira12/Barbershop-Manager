using BarberShopManager.Communication.Clients.Response;

namespace BarberShopManager.Application.UseCases.Clients.GetAll;
public interface IGetAllClientsUseCase
{
    public Task<ResponseClientsJson> Execute();
}
