using BarberShopManager.Domain.Entities;
using BarberShopManager.Domain.Repositories.Services;
using Microsoft.EntityFrameworkCore;

namespace BarberShopManager.Infrastructure.DataAccess.Repositories;
public class ServicesRepository : IServiceWriteRepository, IServiceReadRepository, IServiceUpdateRepository, IServiceDeleteRepository
{
    private BarberShopManagerDbContext _context;

    public ServicesRepository(BarberShopManagerDbContext context)
    {
        _context = context;
    }

    public async Task CreateService(Service service)
    {
        await _context.Services.AddAsync(service);
    }

    public async Task<bool> Delete(int id)
    {
        var service = await _context.Services.FirstOrDefaultAsync(s => s.Id == id);

        if(service is null)
        {
            return false;
        }

        _context.Services.Remove(service);

        return true;
    }

    public async Task<Service> GetServiceById(int id)
    {
        return await _context.Services.AsNoTracking().FirstOrDefaultAsync(service => service.Id == id);
    }

    public void UpdateService(Service service)
    {
        _context.Services.Update(service);
    }
}
