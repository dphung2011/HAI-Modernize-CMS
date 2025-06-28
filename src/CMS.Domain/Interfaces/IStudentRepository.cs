using CMS.Domain.Entities;

namespace CMS.Domain.Interfaces;

public interface IStudentRepository : IRepository<Student>
{
    Task<IEnumerable<Student>> GetByCourseName(string courseName);
    Task<bool> ExistsByIdAsync(int id);
}