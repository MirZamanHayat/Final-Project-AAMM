// UserRepository.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserApi.Entities;
using UserApi.Exceptions;
using UserApi1.Models;

namespace UserApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDbContext _context;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(UserDbContext context, ILogger<UserRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> GetUserByIdAsync(string id)
        {
            if (!Guid.TryParse(id, out var guidId))
            {
                _logger.LogError("Invalid GUID received. Id: {Id}", id);
                return null;
            }
            return await _context.Users.FindAsync(guidId);
        }

        public async Task<User?> CreateUserAsync(User user)
        {
            try
            {
                var createdUser = (await _context.Users.AddAsync(user)).Entity;
                await _context.SaveChangesAsync();

                if (createdUser == null)
                {
                    _logger.LogError("An error occurred while creating user. User data: {User}", user);
                    return null;
                }

                return createdUser;
            }
            catch (DbUpdateException ex) when (ex.InnerException is PostgresException pgEx && pgEx.SqlState == "23505")
            {
                _logger.LogWarning("Attempted to create a user with a duplicate email. Email: {Email}", user.Email);
                throw new DuplicateEmailException("This email is already in use.", ex);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "An error occurred while saving the entity changes.");
                throw new UserCreationFailedException("An unexpected error occurred while creating the user.", ex);
            }
        }

        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task AddUser(User newUser)
        {
            try
            {
                await _context.Users.AddAsync(newUser);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex) when (ex.InnerException is PostgresException pgEx && pgEx.SqlState == "23505")
            {
                _logger.LogWarning("Attempted to create a user with a duplicate email. Email: {Email}", newUser.Email);
                throw new DuplicateEmailException("This email is already in use.", ex);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "An error occurred while saving the entity changes.");
                throw new UserCreationFailedException("An unexpected error occurred while creating the user.", ex);
            }
        }
    }
}
