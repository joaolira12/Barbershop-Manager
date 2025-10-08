using BarberShopManager.Domain.Entities;
using BarberShopManager.Domain.Repositories.Clients;
using Microsoft.EntityFrameworkCore;

namespace BarberShopManager.Infrastructure.DataAccess.Repositories;
public class ClientsRepository : IClientWriteRepository, IClientReadRepository, IClientDeleteRepository, IClientUpdateRepository
{
    private BarberShopManagerDbContext _context;

    public ClientsRepository(BarberShopManagerDbContext context)
    {
        _context = context;
    }

    public async Task CreateClient(Client client)
    {
       await _context.Clients.AddAsync(client);
    }

    public async Task<List<Client>> GetAllClients()
    {
        return await _context.Clients.AsNoTracking().ToListAsync();
    }

    async Task<Client?> IClientReadRepository.GetClientById(int id)
    {
        return await _context.Clients.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
    }

    async Task<Client?> IClientUpdateRepository.GetClientById(int id)
    {
        return await _context.Clients.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<bool> Delete(int id)
    {
        var client = await _context.Clients.FirstOrDefaultAsync(c => c.Id == id);

        if (client == null)
        {
            return false;
        }

        _context.Clients.Remove(client);

        return true;
    }

    public void UpdateClient(Client client)
    {
        _context.Clients.Update(client);
    }
}
