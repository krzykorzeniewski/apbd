using Apbd10.Models;
using Microsoft.EntityFrameworkCore;

namespace Apbd10.Contexts;

public class MyDatabaseContext : DbContext
{
    protected MyDatabaseContext()
    {
    }

    public MyDatabaseContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductCategory> ProductCategories { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<ProductAccount> ProductAccounts { get; set; }
}