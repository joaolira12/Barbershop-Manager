using BarberShopManager.Domain.Entities;
using System.Reflection.Metadata;

namespace BarberShopManager.Domain.Repositories.Clients;
public interface IClientWriteRepository
{
    public Task CreateClient(Client client);
}
