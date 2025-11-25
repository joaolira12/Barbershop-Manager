using AutoMapper;
using BarberShopManager.Communication.Services.Response;
using BarberShopManager.Domain.Repositories.Services;

namespace BarberShopManager.Application.UseCases.Services.GetByWeek;
public class GetServicesLastWeekUseCase : IGetServicesLastWeekUseCase
{
    private readonly IServiceReadRepository _repository;
    private readonly IMapper _mapper;

    public GetServicesLastWeekUseCase(IServiceReadRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ResponseServicesJson> Execute()
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        var sevenDaysAgo = today.AddDays(-7);

        var services = await _repository.FilterLastWeek(sevenDaysAgo, today);

        var response = new ResponseServicesJson(_mapper.Map<List<ResponseShortServiceJson>>(services));

        return response;
    }
}
