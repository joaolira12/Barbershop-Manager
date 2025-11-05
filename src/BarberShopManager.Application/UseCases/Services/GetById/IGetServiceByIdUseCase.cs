using BarberShopManager.Communication.Services.Response;

namespace BarberShopManager.Application.UseCases.Services.GetById;
public interface IGetServiceByIdUseCase
{
    public Task<ResponseServiceJson> Execute(int id);
}
