using Microsoft.EntityFrameworkCore;
using PrzykladoweKolokwium2_1.Models;

namespace PrzykladoweKolokwium2_1.Context;

public class MyContext : DbContext
{
    protected MyContext()
    {
    }

    public MyContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<ProductOrder> ProductOrders { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Status> Statuses { get; set; }
    public DbSet<Client> Clients { get; set; }
}