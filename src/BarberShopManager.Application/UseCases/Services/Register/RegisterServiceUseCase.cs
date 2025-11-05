using AutoMapper;
using BarberShopManager.Application.Validators;
using BarberShopManager.Communication.Services.Request;
using BarberShopManager.Communication.Services.Response;
using BarberShopManager.Domain.Entities;
using BarberShopManager.Domain.Repositories;
using BarberShopManager.Domain.Repositories.Services;
using BarberShopManager.Exceptions.Exceptions;

namespace BarberShopManager.Application.UseCases.Services.Register;
public class RegisterServiceUseCase : IRegisterServiceUseCase
{
    private readonly IServiceWriteRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public RegisterServiceUseCase(IServiceWriteRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ResponseShortServiceJson> Execute(RequestServiceJson request)
    {
        Validate(request);

        //Não foi possível fazer o mapeamento pelo automapping por serem dois enums de projetos diferentes
        Service service = new Service((Domain.Entities.Enums.ServiceType)request.ServiceType, request.Value, request.Date, request.Observation, request.ClientId);

        await _repository.CreateService(service);
        await _unitOfWork.Commit();

        return _mapper.Map<ResponseShortServiceJson>(service);

    }

    private void Validate(RequestServiceJson request)
    {
        var validator = new ServiceValidator();
        var result = validator.Validate(request);

        if (result.IsValid is false)
        {
            List<string> errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();

            throw new ErrorOnValidationServiceException(errorMessages);
        }
    }
}
