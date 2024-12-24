using courses.Models;
using Microsoft.EntityFrameworkCore;

namespace courses.Data;

public class DataContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseSerialColumns();
        modelBuilder.Ignore<Lesson>();

        modelBuilder.Entity<Schedule>()
        .HasKey(s => s.ScheduleId);

        modelBuilder.Entity<Schedule>()
            .Property(s => s.Monday)
            .HasColumnType("jsonb");

        modelBuilder.Entity<Schedule>()
            .Property(s => s.Tuesday)
            .HasColumnType("jsonb");

        modelBuilder.Entity<Schedule>()
            .Property(s => s.Wednesday)
            .HasColumnType("jsonb");

        modelBuilder.Entity<Schedule>()
            .Property(s => s.Thursday)
            .HasColumnType("jsonb");

        modelBuilder.Entity<Schedule>()
            .Property(s => s.Friday)
            .HasColumnType("jsonb");

        modelBuilder.Entity<Schedule>()
            .Property(s => s.Saturday)
            .HasColumnType("jsonb");
        // modelBuilder.Ignore<LessonDay>();
    }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }
    public DbSet<Schedule> Schedules { get; set; }
}
