// IUserService.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using UserApi.Models;
using UserApi.Models;

namespace UserApi.Services
{
    public interface IUserService
    {
        Task<AuthenticateResponse?> Authenticate(AuthenticateRequest model);
        Task<IEnumerable<UserResponse>> GetAllAsync();
        Task<UserResponse?> GetByIdAsync(string id);
        Task<CreateUserResponse?> CreateUserAsync(CreateUserRequest user);
        Task RegisterUser(RegisterUserRequest registrationRequest);
    }
}
