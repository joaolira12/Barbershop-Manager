namespace BarberShopManager.Application.UseCases.Clients.Delete;
public interface IDeleteClientUseCase
{
    public Task Execute(int id);
}
