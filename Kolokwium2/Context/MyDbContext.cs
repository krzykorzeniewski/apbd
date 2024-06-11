using Microsoft.EntityFrameworkCore;

namespace Kolokwium2.Context;

public class MyDbContext : DbContext
{
    protected MyDbContext()
    {
    }

    public MyDbContext(DbContextOptions options) : base(options)
    {
    }
}