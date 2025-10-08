
using BarberShopManager.Domain.Entities;

namespace BarberShopManager.Domain.Repositories.Services;
public interface IServiceWriteRepository
{
    public Task CreateService(Service service);
}
