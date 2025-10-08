using BarberShopManager.Domain.Repositories;

namespace BarberShopManager.Infrastructure.DataAccess.Repositories;
public class UnitOfWork : IUnitOfWork
{
    private BarberShopManagerDbContext _context;

    public UnitOfWork(BarberShopManagerDbContext context)
    {
        _context = context;
    }

    public async Task Commit()
    {
        await _context.SaveChangesAsync();
    }
}
