using Bmp.Domain.Entities;

namespace Bmp.Domain.Repositories;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByIdAsync(Guid id);
    Task<List<User>> GetAllAsync();
    Task AddAsync(User user);
    Task SaveChangesAsync();
    Task<User> UpdateAsync(User user);
}