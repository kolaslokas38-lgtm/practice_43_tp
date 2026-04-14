using JournalApp.Models;
using Microsoft.EntityFrameworkCore;

namespace JournalApp.Data
{
    public class JournalDbContext : DbContext
    {
        public DbSet<StudentModel> Students => Set<StudentModel>();
        public DbSet<CourseModel> Courses => Set<CourseModel>();
        public DbSet<EnrollmentModel> Enrollments => Set<EnrollmentModel>();
        public DbSet<GradeModel> Grades => Set<GradeModel>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=journal.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EnrollmentModel>()
                .HasIndex(e => new { e.StudentId, e.CourseId })
                .IsUnique();

            modelBuilder.Entity<EnrollmentModel>()
                .HasOne(e => e.Student)
                .WithMany(s => s.Enrollments)
                .HasForeignKey(e => e.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<EnrollmentModel>()
                .HasOne(e => e.Course)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(e => e.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<GradeModel>()
                .HasOne(g => g.Enrollment)
                .WithMany(e => e.Grades)
                .HasForeignKey(g => g.EnrollmentId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

