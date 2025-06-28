using CMS.Domain.Entities;

namespace CMS.Domain.Interfaces;

public interface IFacultyRepository : IRepository<Faculty>
{
    Task<IEnumerable<Faculty>> GetBySubjectAsync(string subjectName);
    Task<bool> ExistsByIdAsync(int id);
}