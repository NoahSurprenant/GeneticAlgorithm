using Microsoft.EntityFrameworkCore;
using TimeTable.Core.Models;

namespace TimeTable.Core;
public class DataContext : DbContext
{
    public DbSet<StudentCourse> StudentCourse { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Place> Places { get; set; }
    public DbSet<TimeSlot> TimeSlots { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            @"Server=localhost;Database=TimeTableDb;Integrated Security=True;Encrypt=False");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<StudentCourse>().HasKey("StudentId", "CourseId");
        modelBuilder.Entity<Student>().HasMany(student => student.Courses);
        modelBuilder.Entity<Course>().HasMany(course => course.Students);
        modelBuilder.Entity<Student>().HasMany<TimeSlot>(student => student.TimeSlots);
        modelBuilder.Entity<TimeSlot>().HasMany<Student>(slot => slot.Students);

        base.OnModelCreating(modelBuilder);
    }

}
