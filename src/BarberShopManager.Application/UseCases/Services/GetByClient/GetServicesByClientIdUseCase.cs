using AutoMapper;
using BarberShopManager.Communication.Services.Response;
using BarberShopManager.Domain.Repositories.Services;

namespace BarberShopManager.Application.UseCases.Services.GetByClient;
public class GetServicesByClientIdUseCase : IGetServicesByClientIdUseCase
{
    private readonly IServiceReadRepository _repository;
    private readonly IMapper _mapper;

    public GetServicesByClientIdUseCase(IServiceReadRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ResponseServicesJson> Execute(int clientId)
    {
        var services = await _repository.FilterByClient(clientId);

        var response = new ResponseServicesJson(_mapper.Map<List<ResponseShortServiceJson>>(services));

        return response;
    }
}
