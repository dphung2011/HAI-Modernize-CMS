using CMS.Domain.Entities;
using CMS.Domain.Interfaces;
using CMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CMS.Infrastructure.Repositories;

public class CourseRepository : Repository<Course>, ICourseRepository
{
    public CourseRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Course?> GetByNameAsync(string name)
    {
        return await _context.Courses
            .FirstOrDefaultAsync(c => c.CourseName == name);
    }

    public async Task<bool> ExistsByNameAsync(string name)
    {
        return await _context.Courses.AnyAsync(c => c.CourseName == name);
    }
}