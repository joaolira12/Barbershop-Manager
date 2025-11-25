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

    public async Task<List<Service>> FilterByClient(int clientId)
    {
        return await _context.Services.AsNoTracking().Where(s => s.ClientId == clientId).OrderBy(s => s.Date).ToListAsync();
    }

    public async Task<List<Service>> FilterByMonth(DateOnly date)
    {
        var dateTime = new DateTime(date.Year, date.Month, date.Day);

        return await _context.Services.AsNoTracking().Where(s => s.Date.Month.Equals(dateTime.Month)
        && s.Date.Year.Equals(dateTime.Year)).OrderBy(service => service.Date).ToListAsync();
    }

    public async Task<List<Service>> FilterLastWeek(DateOnly dateStart, DateOnly dateEnd)
    {
        var dateStartTime = dateStart.ToDateTime(TimeOnly.MinValue);
        var dateEndTime = dateEnd.ToDateTime(TimeOnly.MaxValue);

        return await _context.Services.Where(s => s.Date >= dateStartTime 
        && s.Date <= dateEndTime).OrderBy(service => service.Date).ToListAsync();
    }


    async Task<Service?> IServiceReadRepository.GetServiceById(int id)
    {
        return await _context.Services.AsNoTracking().FirstOrDefaultAsync(service => service.Id == id);
    }

    async Task<Service?> IServiceUpdateRepository.GetServiceById(int id)
    {
        return await _context.Services.FirstOrDefaultAsync(service => service.Id == id);
    }

    public void UpdateService(Service service)
    {
        _context.Services.Update(service);
    }
}
