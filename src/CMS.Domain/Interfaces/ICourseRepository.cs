using CMS.Domain.Entities;

namespace CMS.Domain.Interfaces;

public interface ICourseRepository : IRepository<Course>
{
    Task<Course?> GetByNameAsync(string name);
    Task<bool> ExistsByNameAsync(string name);
}