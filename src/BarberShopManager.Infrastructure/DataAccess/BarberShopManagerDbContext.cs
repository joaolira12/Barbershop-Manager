using BarberShopManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BarberShopManager.Infrastructure.DataAccess;
public class BarberShopManagerDbContext : DbContext
{
    public BarberShopManagerDbContext(DbContextOptions options) : base(options) { }

    public DbSet<Client> Clients { get; set; }
    public DbSet<Service> Services { get; set; }
}
