namespace BarberShopManager.Domain.Repositories;
public interface IUnitOfWork
{
    public Task Commit();
}
