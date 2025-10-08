using BarberShopManager.Domain.Entities;

namespace BarberShopManager.Domain.Repositories.Clients;
public interface IClientUpdateRepository
{
    public Task<Client> GetClientById(int id);
    public void UpdateClient(Client client);
}
