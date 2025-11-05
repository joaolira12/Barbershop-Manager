using BarberShopManager.Communication.Services.Response;

namespace BarberShopManager.Application.UseCases.Services.GetByMonth;
public interface IGetServicesByMonthUseCase
{
    public Task<ResponseServicesJson> Execute(DateOnly date);
}
