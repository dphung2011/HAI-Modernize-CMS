using CMS.Domain.Entities;
using CMS.Domain.Interfaces;
using CMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CMS.Infrastructure.Repositories;

public class MarkRepository : Repository<Mark>, IMarkRepository
{
    public MarkRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Mark?> GetByStudentIdAsync(int studentId)
    {
        return await _context.Marks
            .FirstOrDefaultAsync(m => m.StudentId == studentId);
    }

    public async Task<IEnumerable<Mark>> GetByCourseAsync(string courseName)
    {
        return await _context.Marks
            .Where(m => m.CourseName == courseName)
            .ToListAsync();
    }

    public async Task<bool> ExistsForStudentAsync(int studentId)
    {
        return await _context.Marks.AnyAsync(m => m.StudentId == studentId);
    }
}