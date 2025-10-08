using AutoMapper;
using BarberShopManager.Communication.Clients.Response;
using BarberShopManager.Domain.Repositories.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShopManager.Application.UseCases.Clients.GetAll;
internal class GetAllClientsUseCase : IGetAllClientsUseCase
{
    private readonly IClientReadRepository _repository;
    private readonly IMapper _mapper;

    public GetAllClientsUseCase(IClientReadRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ResponseClientsJson> Execute()
    {
        var clients = await _repository.GetAllClients();

        var response = new ResponseClientsJson(_mapper.Map<List<ResponseShortClientJson>>(clients));

        return response;
    }
}
