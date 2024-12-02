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
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductsCategories> ProductsCategories { get; set; }
    public DbSet<Purchase> Purchases { get; set; }
    public DbSet<FileModel> Files { get; set; }
    public DbSet<PurchaseProducts> PurchaseProducts { get; set; }

    }
}
