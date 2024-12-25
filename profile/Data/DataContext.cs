using profile.Models;
using Microsoft.EntityFrameworkCore;

namespace profile.Data;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseSerialColumns();
    }
    public DbSet<User> Users { get; set; }
}
