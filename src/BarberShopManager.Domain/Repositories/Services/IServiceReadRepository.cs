using BarberShopManager.Domain.Entities;

namespace BarberShopManager.Domain.Repositories.Services;
public interface IServiceReadRepository
{
    public Task<Service> GetServiceById(int id);
    public Task<List<Service>> FilterByMonth(DateOnly date);
    public Task<List<Service>> FilterByWeek(DateOnly date);
    public Task<List<Service>> FilterByClient(int clientId);
}
