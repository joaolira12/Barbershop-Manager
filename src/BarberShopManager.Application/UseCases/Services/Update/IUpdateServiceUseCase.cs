using BarberShopManager.Communication.Services.Request;

namespace BarberShopManager.Application.UseCases.Services.Update;
public interface IUpdateServiceUseCase
{
    public Task Execute(int id, RequestServiceJson request);
}
