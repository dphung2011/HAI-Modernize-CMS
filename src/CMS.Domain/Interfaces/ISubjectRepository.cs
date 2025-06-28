using CMS.Domain.Entities;

namespace CMS.Domain.Interfaces;

public interface ISubjectRepository : IRepository<Subject>
{
    Task<IEnumerable<Subject>> GetByCourseAsync(string courseName);
    Task<Subject?> GetByNameAsync(string name);
}