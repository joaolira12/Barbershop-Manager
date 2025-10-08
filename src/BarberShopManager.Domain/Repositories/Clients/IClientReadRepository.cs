using BarberShopManager.Domain.Entities;

namespace BarberShopManager.Domain.Repositories.Clients;
public interface IClientReadRepository
{
    public Task<List<Client>> GetAllClients();
    public Task<Client> GetClientById(int id);
}
