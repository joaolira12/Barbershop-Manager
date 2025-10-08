using BarberShopManager.Communication.Clients.Response;

namespace BarberShopManager.Application.UseCases.Clients.GetById;
public interface IGetClientByIdUseCase
{
    public Task<ResponseClientJson> Execute(int id);
}
