using CMS.Domain.Entities;

namespace CMS.Domain.Interfaces;

public interface IAttendanceRepository : IRepository<Attendance>
{
    Task<IEnumerable<Attendance>> GetByStudentIdAsync(int studentId);
    Task<IEnumerable<Attendance>> GetByDateAsync(string date);
    Task<IEnumerable<Attendance>> GetByCourseAndSubjectAsync(string courseName, string subjectName);
}