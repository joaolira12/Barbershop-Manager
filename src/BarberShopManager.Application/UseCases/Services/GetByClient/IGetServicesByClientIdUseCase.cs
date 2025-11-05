using BarberShopManager.Communication.Services.Response;

namespace BarberShopManager.Application.UseCases.Services.GetByClient;
public interface IGetServicesByClientIdUseCase
{
    public Task<ResponseServicesJson> Execute(int clientId);
}
