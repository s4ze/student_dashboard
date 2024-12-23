using courses.Models;
using Microsoft.EntityFrameworkCore;

namespace courses.Data;

public class DataContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseSerialColumns();
    }
    public DbSet<Course> Courses { get; set; }
}
