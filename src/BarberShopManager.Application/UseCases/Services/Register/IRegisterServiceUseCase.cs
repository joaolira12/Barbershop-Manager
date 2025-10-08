using BarberShopManager.Communication.Services.Request;
using BarberShopManager.Communication.Services.Response;

namespace BarberShopManager.Application.UseCases.Services.Register;
public interface IRegisterServiceUseCase
{
    public Task<ResponseShortServiceJson> Execute(RequestServiceJson request);
}
