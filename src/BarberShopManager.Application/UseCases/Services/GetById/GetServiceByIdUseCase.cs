using AutoMapper;
using BarberShopManager.Communication.Services.Response;
using BarberShopManager.Domain.Repositories.Services;
using BarberShopManager.Exceptions.Exceptions;
using BarberShopManager.Exceptions.Resources;

namespace BarberShopManager.Application.UseCases.Services.GetById;
public class GetServiceByIdUseCase : IGetServiceByIdUseCase
{
    private readonly IServiceReadRepository _repository;
    private readonly IMapper _mapper;

    public GetServiceByIdUseCase(IServiceReadRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ResponseServiceJson> Execute(int id)
    {
        var response = await _repository.GetServiceById(id);

        if(response == null)
        {
            throw new NotFoundServiceException(ResourceErrorMessages.SERVICE_NOT_FOUND);
        }

        return new ResponseServiceJson(response.Id, (Communication.Services.Enums.ServiceType)response.ServiceType, response.Value, response.Date, response.Observation);
    }
}
