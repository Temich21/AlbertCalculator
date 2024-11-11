using Microsoft.EntityFrameworkCore;
using AlbertCalculator.Models;

namespace AlbertCalculator.Data
{
public class AlbertCalculatorDataContext : DbContext
{
    public AlbertCalculatorDataContext(DbContextOptions<AlbertCalculatorDataContext> options) :
        base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("albercalculatorschema");

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Category> Category { get; set; }
    public DbSet<Product> Product { get; set; }
    public DbSet<ProductsCategories> ProductsCategories { get; set; }
    public DbSet<Purchase> Purchase { get; set; }
    public DbSet<PurchaseProducts> PurchaseProducts { get; set; }

    }
}
