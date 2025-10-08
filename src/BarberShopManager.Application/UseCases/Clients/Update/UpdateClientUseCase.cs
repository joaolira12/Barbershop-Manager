
using AutoMapper;
using BarberShopManager.Application.Validators;
using BarberShopManager.Communication.Clients.Request;
using BarberShopManager.Domain.Entities;
using BarberShopManager.Domain.Repositories;
using BarberShopManager.Domain.Repositories.Clients;
using BarberShopManager.Exceptions.Exceptions;
using BarberShopManager.Exceptions.Resources;

namespace BarberShopManager.Application.UseCases.Clients.Update;
public class UpdateClientUseCase : IUpdateClientUseCase
{
    private readonly IClientUpdateRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateClientUseCase(IClientUpdateRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task Execute(int id, RequestClientJson request)
    {
        Validate(request);

        var client = await _repository.GetClientById(id);

        if(client == null)
        {
            throw new NotFoundClientException(ResourceErrorMessages.CLIENT_NOT_FOUND);
        }

        _repository.UpdateClient(_mapper.Map(request, client));

        await _unitOfWork.Commit();

    }

    private void Validate(RequestClientJson request)
    {
        var validator = new ClientValidator();
        var result = validator.Validate(request);

        if (result.IsValid is false)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

            throw new ErrorOnValidationClientException(errorMessages);
        }

    }
}
