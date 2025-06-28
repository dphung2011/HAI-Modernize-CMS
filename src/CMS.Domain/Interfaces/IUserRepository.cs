using CMS.Domain.Entities;

namespace CMS.Domain.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByUsernameAsync(string username);
    Task<bool> IsUsernameUniqueAsync(string username);
}