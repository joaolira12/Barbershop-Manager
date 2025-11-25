using BarberShopManager.Communication.Services.Response;

namespace BarberShopManager.Application.UseCases.Services.GetByWeek;
public interface IGetServicesLastWeekUseCase
{
    public Task<ResponseServicesJson> Execute();
}
