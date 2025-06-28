using CMS.Domain.Entities;
using CMS.Domain.Interfaces;
using CMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CMS.Infrastructure.Repositories;

public class SubjectRepository : Repository<Subject>, ISubjectRepository
{
    public SubjectRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Subject>> GetByCourseAsync(string courseName)
    {
        return await _context.Subjects
            .Where(s => s.CourseName == courseName)
            .ToListAsync();
    }

    public async Task<Subject?> GetByNameAsync(string name)
    {
        return await _context.Subjects
            .FirstOrDefaultAsync(s => s.SubjectName == name);
    }
}