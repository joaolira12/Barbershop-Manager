using BarberShopManager.Domain.Entities;

namespace BarberShopManager.Domain.Repositories.Services;
public interface IServiceUpdateRepository
{
    public Task<Service> GetServiceById(int id);
    public void UpdateService(Service service);
}
