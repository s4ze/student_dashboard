using messages.Models;
using Microsoft.EntityFrameworkCore;

namespace messages.Data;

public class DataContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseSerialColumns();
    }
    public DbSet<Message> Messages { get; set; }
}
