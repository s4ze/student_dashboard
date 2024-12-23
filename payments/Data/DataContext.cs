using payments.Models;
using Microsoft.EntityFrameworkCore;

namespace payments.Data;

public class DataContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseSerialColumns();
    }
    public DbSet<Payment> Payments { get; set; }
}
