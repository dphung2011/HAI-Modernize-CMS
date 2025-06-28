using CMS.Domain.Entities;

namespace CMS.Domain.Interfaces;

public interface IMarkRepository : IRepository<Mark>
{
    Task<Mark?> GetByStudentIdAsync(int studentId);
    Task<IEnumerable<Mark>> GetByCourseAsync(string courseName);
    Task<bool> ExistsForStudentAsync(int studentId);
}