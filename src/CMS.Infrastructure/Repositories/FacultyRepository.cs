using CMS.Domain.Entities;
using CMS.Domain.Interfaces;
using CMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CMS.Infrastructure.Repositories;

public class FacultyRepository : Repository<Faculty>, IFacultyRepository
{
    public FacultyRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Faculty>> GetBySubjectAsync(string subjectName)
    {
        return await _context.Faculties
            .Where(f => f.SubjectName == subjectName)
            .ToListAsync();
    }

    public async Task<bool> ExistsByIdAsync(int id)
    {
        return await _context.Faculties.AnyAsync(f => f.FacultyId == id);
    }
}