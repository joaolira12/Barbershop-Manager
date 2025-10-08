using AutoMapper;
using BarberShopManager.Communication.Clients.Response;
using BarberShopManager.Domain.Repositories.Clients;
using BarberShopManager.Exceptions.Exceptions;
using BarberShopManager.Exceptions.Resources;

namespace BarberShopManager.Application.UseCases.Clients.GetById;
public class GetClientByIdUseCase : IGetClientByIdUseCase
{
    private readonly IClientReadRepository _repository;
    private readonly IMapper _mapper;

    public GetClientByIdUseCase(IClientReadRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ResponseClientJson> Execute(int id)
    {
        var client = await _repository.GetClientById(id);

        if(client == null)
        {
            throw new NotFoundClientException(ResourceErrorMessages.CLIENT_NOT_FOUND);
        }

        return _mapper.Map<ResponseClientJson>(client);

    }
}
