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
    public DbSet<Enrollment> Enrollments { get; set; }
    public DbSet<Schedule> Schedules { get; set; }
    public DbSet<LessonDay> LessonDays { get; set; }
    public DbSet<Lesson> Lessons { get; set; }
    public DbSet<Classroom> Classrooms { get; set; }

}
