using Microsoft.EntityFrameworkCore;
using Products.Api.Entities;

namespace Products.Api;

public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options)
        : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
}
