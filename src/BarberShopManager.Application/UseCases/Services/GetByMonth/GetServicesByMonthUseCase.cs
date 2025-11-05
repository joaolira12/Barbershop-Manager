using AutoMapper;
using BarberShopManager.Communication.Services.Response;
using BarberShopManager.Domain.Repositories.Services;

namespace BarberShopManager.Application.UseCases.Services.GetByMonth;
public class GetServicesByMonthUseCase : IGetServicesByMonthUseCase
{
    private readonly IServiceReadRepository _repository;
    private readonly IMapper _mapper;

    public GetServicesByMonthUseCase(IServiceReadRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ResponseServicesJson> Execute(DateOnly date)
    {
        var services = await _repository.FilterByMonth(date);

        var response = new ResponseServicesJson(_mapper.Map<List<ResponseShortServiceJson>>(services));

        return response;
    }
}
