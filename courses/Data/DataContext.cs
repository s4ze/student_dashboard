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
            .OwnsOne(s => s.Monday, ld =>
            {
                ld.ToJson();
                ld.OwnsOne(ld => ld.Lesson1);
                ld.OwnsOne(ld => ld.Lesson2);
                ld.OwnsOne(ld => ld.Lesson3);
                ld.OwnsOne(ld => ld.Lesson4);
                ld.OwnsOne(ld => ld.Lesson5);
            });

        modelBuilder.Entity<Schedule>()
            .OwnsOne(s => s.Tuesday, ld =>
            {
                ld.ToJson();
                ld.OwnsOne(ld => ld.Lesson1);
                ld.OwnsOne(ld => ld.Lesson2);
                ld.OwnsOne(ld => ld.Lesson3);
                ld.OwnsOne(ld => ld.Lesson4);
                ld.OwnsOne(ld => ld.Lesson5);
            });

        modelBuilder.Entity<Schedule>()
            .OwnsOne(s => s.Wednesday, ld =>
            {
                ld.ToJson();
                ld.OwnsOne(ld => ld.Lesson1);
                ld.OwnsOne(ld => ld.Lesson2);
                ld.OwnsOne(ld => ld.Lesson3);
                ld.OwnsOne(ld => ld.Lesson4);
                ld.OwnsOne(ld => ld.Lesson5);
            });

        modelBuilder.Entity<Schedule>()
            .OwnsOne(s => s.Thursday, ld =>
            {
                ld.ToJson();
                ld.OwnsOne(ld => ld.Lesson1);
                ld.OwnsOne(ld => ld.Lesson2);
                ld.OwnsOne(ld => ld.Lesson3);
                ld.OwnsOne(ld => ld.Lesson4);
                ld.OwnsOne(ld => ld.Lesson5);
            });

        modelBuilder.Entity<Schedule>()
            .OwnsOne(s => s.Friday, ld =>
            {
                ld.ToJson();
                ld.OwnsOne(ld => ld.Lesson1);
                ld.OwnsOne(ld => ld.Lesson2);
                ld.OwnsOne(ld => ld.Lesson3);
                ld.OwnsOne(ld => ld.Lesson4);
                ld.OwnsOne(ld => ld.Lesson5);
            });

        modelBuilder.Entity<Schedule>()
            .OwnsOne(s => s.Saturday, ld =>
            {
                ld.ToJson();
                ld.OwnsOne(ld => ld.Lesson1);
                ld.OwnsOne(ld => ld.Lesson2);
                ld.OwnsOne(ld => ld.Lesson3);
                ld.OwnsOne(ld => ld.Lesson4);
                ld.OwnsOne(ld => ld.Lesson5);
            });
    }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }
    public DbSet<Schedule> Schedules { get; set; }
}
