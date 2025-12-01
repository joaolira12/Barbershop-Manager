using AutoMapper;
using BarberShopManager.Application.Validators;
using BarberShopManager.Communication.Clients.Request;
using BarberShopManager.Communication.Clients.Response;
using BarberShopManager.Domain.Entities;
using BarberShopManager.Domain.Repositories;
using BarberShopManager.Domain.Repositories.Clients;
using BarberShopManager.Exceptions.Exceptions;

namespace BarberShopManager.Application.UseCases.Clients.Register;
public class RegisterClientUseCase : IRegisterClientUseCase
{
    private readonly IClientWriteRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public RegisterClientUseCase(IClientWriteRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ResponseRegisteredClientJson> Execute(RequestClientJson request)
    {
        Validate(request);

        var client = _mapper.Map<Client>(request);

        await _repository.CreateClient(client);
        await _unitOfWork.Commit();

        return _mapper.Map<ResponseRegisteredClientJson>(request);
    }

    private void Validate(RequestClientJson request)
    {
        ClientValidator validator = new ClientValidator();

        var result = validator.Validate(request);

        if(result.IsValid is false)
        {
            List<string> errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();

            throw new ErrorOnValidationClientException(errorMessages);
        }
    }
}
