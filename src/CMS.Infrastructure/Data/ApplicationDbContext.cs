using CMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CMS.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Faculty> Faculties { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<Attendance> Attendances { get; set; }
    public DbSet<Mark> Marks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure User entity
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Username).IsRequired().HasMaxLength(20);
            entity.Property(e => e.Password).IsRequired().HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(20);
            entity.Property(e => e.EmailId).HasMaxLength(45);
            entity.Property(e => e.Phone).HasMaxLength(45);
            entity.Property(e => e.Role).HasMaxLength(20);
        });

        // Configure Student entity
        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentId);
            entity.Property(e => e.FirstName).IsRequired().HasMaxLength(20);
            entity.Property(e => e.LastName).HasMaxLength(30);
            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.Property(e => e.DateOfBirth).HasMaxLength(45);
            entity.Property(e => e.FatherName).HasMaxLength(45);
            entity.Property(e => e.MotherName).HasMaxLength(45);
            entity.Property(e => e.ContactNumber).HasMaxLength(45);
            entity.Property(e => e.EmailId).HasMaxLength(45);
            entity.Property(e => e.Address).HasMaxLength(50);
            entity.Property(e => e.City).HasMaxLength(45);
            entity.Property(e => e.State).HasMaxLength(45);
            entity.Property(e => e.CourseName).HasMaxLength(45);
            entity.Property(e => e.AdmissionDate).HasMaxLength(50);
            entity.Property(e => e.ActiveStatus).HasMaxLength(45);
        });

        // Configure Faculty entity
        modelBuilder.Entity<Faculty>(entity =>
        {
            entity.HasKey(e => e.FacultyId);
            entity.Property(e => e.FacultyFirstName).HasMaxLength(45);
            entity.Property(e => e.FacultyLastName).HasMaxLength(45);
            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.Property(e => e.DateOfBirth).HasMaxLength(45);
            entity.Property(e => e.ContactNumber).HasMaxLength(45);
            entity.Property(e => e.EmailId).HasMaxLength(45);
            entity.Property(e => e.Address).HasMaxLength(50);
            entity.Property(e => e.City).HasMaxLength(45);
            entity.Property(e => e.State).HasMaxLength(45);
            entity.Property(e => e.Qualification).HasMaxLength(45);
            entity.Property(e => e.Experience).HasMaxLength(45);
            entity.Property(e => e.SubjectName).HasMaxLength(45);
            entity.Property(e => e.Password).HasMaxLength(45);
            entity.Property(e => e.ActiveStatus).HasMaxLength(45);
            entity.Property(e => e.JoinDate).HasMaxLength(45);
        });

        // Configure Course entity
        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.CourseId);
            entity.Property(e => e.CourseName).HasMaxLength(30);
            entity.Property(e => e.SemOrYear).HasMaxLength(20);
        });

        // Configure Subject entity
        modelBuilder.Entity<Subject>(entity =>
        {
            entity.HasKey(e => e.SubjectId);
            entity.Property(e => e.CourseName).HasMaxLength(45);
            entity.Property(e => e.SemOrYear).HasMaxLength(30);
            entity.Property(e => e.SubjectName).HasMaxLength(45);
            entity.Property(e => e.SubjectType).HasMaxLength(20);
        });

        // Configure Attendance entity
        modelBuilder.Entity<Attendance>(entity =>
        {
            entity.HasKey(e => e.AttendanceId);
            entity.Property(e => e.Date).HasMaxLength(40);
            entity.Property(e => e.CourseName).HasMaxLength(30);
            entity.Property(e => e.SemOrYear).HasMaxLength(30);
            entity.Property(e => e.SubjectName).HasMaxLength(30);
            entity.Property(e => e.FirstName).HasMaxLength(45);
            entity.Property(e => e.LastName).HasMaxLength(45);
            entity.Property(e => e.Status).IsRequired().HasMaxLength(30);
        });

        // Configure Mark entity
        modelBuilder.Entity<Mark>(entity =>
        {
            entity.HasKey(e => e.MarkId);
            entity.Property(e => e.FirstName).HasMaxLength(45);
            entity.Property(e => e.LastName).HasMaxLength(45);
            entity.Property(e => e.CourseName).HasMaxLength(45);
            entity.Property(e => e.SemOrYear).HasMaxLength(45);
            
            // Subject 1
            entity.Property(e => e.Subject1).HasMaxLength(45);
            entity.Property(e => e.Subject1MaxTheoryMarks).HasMaxLength(45);
            entity.Property(e => e.Subject1TheoryMarks).HasMaxLength(45);
            entity.Property(e => e.Subject1MaxPracticalMarks).HasMaxLength(45);
            entity.Property(e => e.Subject1PracticalMarks).HasMaxLength(45);
            
            // Subject 2
            entity.Property(e => e.Subject2).HasMaxLength(45);
            entity.Property(e => e.Subject2MaxTheoryMarks).HasMaxLength(45);
            entity.Property(e => e.Subject2TheoryMarks).HasMaxLength(45);
            entity.Property(e => e.Subject2MaxPracticalMarks).HasMaxLength(45);
            entity.Property(e => e.Subject2PracticalMarks).HasMaxLength(45);
            
            // Subject 3
            entity.Property(e => e.Subject3).HasMaxLength(45);
            entity.Property(e => e.Subject3MaxTheoryMarks).HasMaxLength(45);
            entity.Property(e => e.Subject3TheoryMarks).HasMaxLength(45);
            entity.Property(e => e.Subject3MaxPracticalMarks).HasMaxLength(45);
            entity.Property(e => e.Subject3PracticalMarks).HasMaxLength(45);
            
            // Subject 4
            entity.Property(e => e.Subject4).HasMaxLength(45);
            entity.Property(e => e.Subject4MaxTheoryMarks).HasMaxLength(45);
            entity.Property(e => e.Subject4TheoryMarks).HasMaxLength(45);
            entity.Property(e => e.Subject4MaxPracticalMarks).HasMaxLength(45);
            entity.Property(e => e.Subject4PracticalMarks).HasMaxLength(45);
            
            // Subject 5
            entity.Property(e => e.Subject5).HasMaxLength(45);
            entity.Property(e => e.Subject5MaxTheoryMarks).HasMaxLength(45);
            entity.Property(e => e.Subject5TheoryMarks).HasMaxLength(45);
            entity.Property(e => e.Subject5MaxPracticalMarks).HasMaxLength(45);
            entity.Property(e => e.Subject5PracticalMarks).HasMaxLength(45);
            
            // Subject 6
            entity.Property(e => e.Subject6).HasMaxLength(45);
            entity.Property(e => e.Subject6MaxTheoryMarks).HasMaxLength(45);
            entity.Property(e => e.Subject6TheoryMarks).HasMaxLength(45);
            entity.Property(e => e.Subject6MaxPracticalMarks).HasMaxLength(45);
            entity.Property(e => e.Subject6PracticalMarks).HasMaxLength(45);
            
            // Totals
            entity.Property(e => e.TotalMaxTheoryMarks).HasMaxLength(45);
            entity.Property(e => e.TotalTheoryMarks).HasMaxLength(45);
            entity.Property(e => e.TotalMaxPracticalMarks).HasMaxLength(45);
            entity.Property(e => e.TotalPracticalMarks).HasMaxLength(45);
        });

        // Configure relationships
        modelBuilder.Entity<Student>()
            .HasOne(s => s.Course)
            .WithMany(c => c.Students)
            .HasForeignKey(s => s.CourseName)
            .HasPrincipalKey(c => c.CourseName);

        modelBuilder.Entity<Faculty>()
            .HasOne(f => f.Subject)
            .WithMany(s => s.Faculties)
            .HasForeignKey(f => f.SubjectName)
            .HasPrincipalKey(s => s.SubjectName);

        modelBuilder.Entity<Subject>()
            .HasOne(s => s.Course)
            .WithMany(c => c.Subjects)
            .HasForeignKey(s => s.CourseName)
            .HasPrincipalKey(c => c.CourseName);

        modelBuilder.Entity<Attendance>()
            .HasOne(a => a.Student)
            .WithMany(s => s.Attendances)
            .HasForeignKey(a => a.StudentId);

        modelBuilder.Entity<Mark>()
            .HasOne(m => m.Student)
            .WithMany(s => s.Marks)
            .HasForeignKey(m => m.StudentId);
    }
}