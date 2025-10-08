namespace BarberShopManager.Domain.Repositories.Clients;
public interface IClientDeleteRepository
{
    public Task<bool> Delete(int id);
}
