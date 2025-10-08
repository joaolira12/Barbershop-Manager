using BarberShopManager.Domain.Repositories;
using BarberShopManager.Domain.Repositories.Clients;
using BarberShopManager.Exceptions.Exceptions;
using BarberShopManager.Exceptions.Resources;

namespace BarberShopManager.Application.UseCases.Clients.Delete;
public class DeleteClientUseCase : IDeleteClientUseCase
{
    private readonly IClientDeleteRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteClientUseCase(IClientDeleteRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(int id)
    {
        var result = await _repository.Delete(id);

        if(result == false)
        {
            throw new NotFoundClientException(ResourceErrorMessages.CLIENT_NOT_FOUND);
        }

        await _unitOfWork.Commit();

    }
}
