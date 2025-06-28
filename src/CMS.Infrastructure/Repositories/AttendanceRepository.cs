using CMS.Domain.Entities;
using CMS.Domain.Interfaces;
using CMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CMS.Infrastructure.Repositories;

public class AttendanceRepository : Repository<Attendance>, IAttendanceRepository
{
    public AttendanceRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Attendance>> GetByStudentIdAsync(int studentId)
    {
        return await _context.Attendances
            .Where(a => a.StudentId == studentId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Attendance>> GetByDateAsync(string date)
    {
        return await _context.Attendances
            .Where(a => a.Date == date)
            .ToListAsync();
    }

    public async Task<IEnumerable<Attendance>> GetByCourseAndSubjectAsync(string courseName, string subjectName)
    {
        return await _context.Attendances
            .Where(a => a.CourseName == courseName && a.SubjectName == subjectName)
            .ToListAsync();
    }
}