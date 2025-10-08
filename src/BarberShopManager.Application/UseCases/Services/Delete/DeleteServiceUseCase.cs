
using AutoMapper;
using BarberShopManager.Domain.Repositories;
using BarberShopManager.Domain.Repositories.Services;
using BarberShopManager.Exceptions.Exceptions;
using BarberShopManager.Exceptions.Resources;

namespace BarberShopManager.Application.UseCases.Services.Delete;
public class DeleteServiceUseCase : IDeleteServiceUseCase
{
    private readonly IServiceDeleteRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteServiceUseCase(IServiceDeleteRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(int id)
    {
        var result = await _repository.Delete(id);

        if(result is false)
        {
            throw new NotFoundServiceException(ResourceErrorMessages.SERVICE_NOT_FOUND);
        }

        await _unitOfWork.Commit();
        
    }
}
