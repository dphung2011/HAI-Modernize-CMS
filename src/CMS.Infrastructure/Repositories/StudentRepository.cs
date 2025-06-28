using CMS.Domain.Entities;
using CMS.Domain.Interfaces;
using CMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CMS.Infrastructure.Repositories;

public class StudentRepository : Repository<Student>, IStudentRepository
{
    public StudentRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Student>> GetByCourseName(string courseName)
    {
        return await _context.Students
            .Where(s => s.CourseName == courseName)
            .ToListAsync();
    }

    public async Task<bool> ExistsByIdAsync(int id)
    {
        return await _context.Students.AnyAsync(s => s.StudentId == id);
    }
}