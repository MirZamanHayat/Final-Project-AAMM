using UserApi.Entities;

namespace UserApi.Repositories;
public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task<User?> GetUserByUsernameAsync(string username);
    Task<User?> GetUserByIdAsync(string id);
    Task<User?> CreateUserAsync(User user);
    Task AddUser(User newUser);
}
