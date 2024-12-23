using authorization.Models;
using Microsoft.EntityFrameworkCore;

namespace authorization.Data;

public class DataContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseSerialColumns();
    }
    public DbSet<User> Users { get; set; }
}
