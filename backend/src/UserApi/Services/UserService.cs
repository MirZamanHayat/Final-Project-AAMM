// UserService.cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using UserApi.Authorization;
using UserApi.Entities;
using UserApi.Helpers;
using UserApi.Models;
using UserApi.Repositories;
using UserApi.Models;

namespace UserApi.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtUtils _jwtUtils;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher _passwordHasher;

        public UserService(IUserRepository userRepository, IJwtUtils jwtUtils, IMapper mapper, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _jwtUtils = jwtUtils;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
        }

        public async Task<AuthenticateResponse?> Authenticate(AuthenticateRequest model)
        {
            var user = await _userRepository.GetUserByUsernameAsync(model.Username);

            if (user == null) return null;

            if (!_passwordHasher.ValidatePassword(model.Password, user.PasswordHash, user.PasswordSalt)) return null;

            // Update LastLoginTime on successful authentication
            user.LastLoginTime = DateTime.UtcNow;
            await _userRepository.UpdateUser(user);

            var token = _jwtUtils.GenerateJwtToken(user);

            return _mapper.Map<AuthenticateResponse>(user, opts => opts.Items["Token"] = token);
        }

        public async Task<CreateUserResponse?> CreateUserAsync(CreateUserRequest userRequest)
        {
            (byte[] passwordHash, byte[] passwordSalt) = _passwordHasher.HashPassword(userRequest.Password);

            var userEntity = _mapper.Map<User>(userRequest);

            userEntity.PasswordHash = passwordHash;
            userEntity.PasswordSalt = passwordSalt;

            var createdUser = await _userRepository.CreateUserAsync(userEntity)
                ?? throw new Exception("An error occurred when creating user. Try again later.");

            return _mapper.Map<CreateUserResponse>(createdUser);
        }

        public async Task<IEnumerable<UserResponse>> GetAllAsync()
        {
            var users = await _userRepository.GetAllUsersAsync();

            return _mapper.Map<IEnumerable<UserResponse>>(users);
        }

        public async Task<UserResponse?> GetByIdAsync(string id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            return _mapper.Map<UserResponse>(user);
        }

        public async Task RegisterUser(RegisterUserRequest registrationRequest)
        {
            var newUser = _mapper.Map<User>(registrationRequest);
            await _userRepository.AddUser(newUser);
        }
    }
}
