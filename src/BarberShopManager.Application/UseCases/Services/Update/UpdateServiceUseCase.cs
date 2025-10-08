using AutoMapper;
using BarberShopManager.Application.Validators;
using BarberShopManager.Communication.Services.Request;
using BarberShopManager.Domain.Entities;
using BarberShopManager.Domain.Repositories;
using BarberShopManager.Domain.Repositories.Services;
using BarberShopManager.Exceptions.Exceptions;
using BarberShopManager.Exceptions.Resources;

namespace BarberShopManager.Application.UseCases.Services.Update;
public class UpdateServiceUseCase : IUpdateServiceUseCase
{
    private readonly IServiceUpdateRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateServiceUseCase(IServiceUpdateRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task Execute(int id, RequestServiceJson request)
    {

        Validate(request);

        var service = await _repository.GetServiceById(id);

        if (service is null)
        {
            throw new NotFoundServiceException(ResourceErrorMessages.SERVICE_NOT_FOUND);
        }

        _repository.UpdateService(_mapper.Map(request, service));

        await _unitOfWork.Commit();
    }

    private void Validate(RequestServiceJson request)
    {
        var validator = new ServiceValidator();
        var result = validator.Validate(request);

        if (result.IsValid is false)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

            throw new ErrorOnValidationServiceException(errorMessages);
        }

    }
}
