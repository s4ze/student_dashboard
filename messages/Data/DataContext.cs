using System;
using profile.Models;
using Microsoft.EntityFrameworkCore;

namespace profile.Data;

public class DataContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseSerialColumns();
    }
    public DbSet<Message> Messages { get; set; }
}
