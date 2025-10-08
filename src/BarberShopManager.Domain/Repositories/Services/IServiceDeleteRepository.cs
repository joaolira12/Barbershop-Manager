namespace BarberShopManager.Domain.Repositories.Services;
public interface IServiceDeleteRepository
{
    public Task<bool> Delete(int id);
}
