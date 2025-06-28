using CMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CMS.Infrastructure.Data;

public static class DbInitializer
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var context = new ApplicationDbContext(
            serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());
        
        // Create the database if it doesn't exist
        context.Database.EnsureCreated();

        // Check if the database has been seeded
        if (context.Users.Any())
        {
            return;   // Database has been seeded
        }

        // Seed admin user
        var adminUser = new User
        {
            Username = "admin",
            Password = "admin123", // In production, this should be hashed
            LastName = "Administrator",
            EmailId = "admin@cms.com",
            Role = "Admin"
        };

        context.Users.Add(adminUser);
        context.SaveChanges();

        // Seed sample courses
        var courses = new Course[]
        {
            new Course { CourseName = "Computer Science", SemOrYear = "Semester", TotalSemOrYear = 8 },
            new Course { CourseName = "Information Technology", SemOrYear = "Semester", TotalSemOrYear = 8 },
            new Course { CourseName = "Electrical Engineering", SemOrYear = "Semester", TotalSemOrYear = 8 },
            new Course { CourseName = "Mechanical Engineering", SemOrYear = "Semester", TotalSemOrYear = 8 }
        };

        context.Courses.AddRange(courses);
        context.SaveChanges();

        // Seed sample subjects
        var subjects = new Subject[]
        {
            new Subject { CourseName = "Computer Science", SemOrYear = "1", SubjectName = "Introduction to Programming", SubjectType = "Both", TheoryMarks = 70, PracticalMarks = 30 },
            new Subject { CourseName = "Computer Science", SemOrYear = "1", SubjectName = "Mathematics I", SubjectType = "Theory", TheoryMarks = 100, PracticalMarks = 0 },
            new Subject { CourseName = "Computer Science", SemOrYear = "1", SubjectName = "Digital Electronics", SubjectType = "Both", TheoryMarks = 60, PracticalMarks = 40 },
            new Subject { CourseName = "Information Technology", SemOrYear = "1", SubjectName = "Introduction to IT", SubjectType = "Both", TheoryMarks = 70, PracticalMarks = 30 },
            new Subject { CourseName = "Information Technology", SemOrYear = "1", SubjectName = "Web Development", SubjectType = "Both", TheoryMarks = 50, PracticalMarks = 50 }
        };

        context.Subjects.AddRange(subjects);
        context.SaveChanges();

        // Seed sample faculty
        var faculty = new Faculty
        {
            FacultyFirstName = "John",
            FacultyLastName = "Doe",
            Gender = "Male",
            DateOfBirth = "1980-01-01",
            Age = 43,
            ContactNumber = "1234567890",
            EmailId = "john.doe@cms.com",
            Address = "123 Faculty Street",
            City = "Faculty City",
            State = "Faculty State",
            Pincode = 123456,
            Qualification = "PhD",
            Experience = "10",
            SubjectName = "Introduction to Programming",
            Password = "faculty123", // In production, this should be hashed
            ActiveStatus = "Active",
            JoinDate = DateTime.Now.ToString("yyyy-MM-dd")
        };

        context.Faculties.Add(faculty);
        context.SaveChanges();
    }
}