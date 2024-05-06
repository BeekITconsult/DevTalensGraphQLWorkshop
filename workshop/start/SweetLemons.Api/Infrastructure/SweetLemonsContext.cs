using Microsoft.EntityFrameworkCore;
using SweetLemons.Api.Entities;

namespace SweetLemons.Api.Infrastructure;

public class SweetLemonsContext : DbContext
{
    public SweetLemonsContext(DbContextOptions<SweetLemonsContext> dbContextOptions)
        : base(dbContextOptions)
    {
    }

    public DbSet<Customer> Customers => Set<Customer>();

    public DbSet<Order> Orders => Set<Order>();

    public DbSet<Product> Products => Set<Product>();

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SweetLemonsContext).Assembly);
    }
}