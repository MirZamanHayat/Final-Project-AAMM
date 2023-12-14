// UserApi.Models.RegisterUserRequest

namespace UserApi.Models;
// RegisterUserRequest.cs
public class RegisterUserRequest
{
    public string Username { get; set; } = string.Empty; // Initialize with a default value
    public string Password { get; set; } = string.Empty; // Initialize with a default value
    // Add other properties as needed for user registration
}
