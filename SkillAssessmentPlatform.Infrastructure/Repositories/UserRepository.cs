using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SkillAssessmentPlatform.Core.Entities.Users;
using SkillAssessmentPlatform.Core.Enums;
using SkillAssessmentPlatform.Core.Exceptions;
using SkillAssessmentPlatform.Core.Interfaces;
using SkillAssessmentPlatform.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillAssessmentPlatform.Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(
            AppDbContext context,
            UserManager<User> userManager,
            ILogger<UserRepository> logger) : base(context)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public override async Task<User> GetByIdAsync(string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user == null)
                {
                    _logger.LogWarning("User with ID {UserId} not found", id);
                    throw new UserNotFoundException($"No user with id: {id}");
                }
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting user by ID {UserId}", id);
                throw;
            }
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    _logger.LogWarning("User with email {Email} not found", email);
                    throw new UserNotFoundException($"No user with email: {email}");
                }
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting user by email {Email}", email);
                throw;
            }
        }

        public async Task<IEnumerable<User>> GetUsersByTypeAsync(Actors userType, int page = 1, int pageSize = 10)
        {
            try
            {
                return await _userManager.Users
                    .Where(u => u.UserType == userType)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting users by type {UserType}", userType);
                throw;
            }
        }

        public async Task<IEnumerable<User>> SearchUsersAsync(string searchTerm, Actors userType)
        {
            try
            {
                var query = _userManager.Users.AsQueryable();

                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    query = query.Where(u =>
                        u.Email.Contains(searchTerm) ||
                        u.UserName.Contains(searchTerm) ||
                        u.FullName.Contains(searchTerm)
                    );
                }

                if (userType.HasValue)
                {
                    query = query.Where(u => u.UserType == userType.Value);
                }

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while searching users with term {SearchTerm}", searchTerm);
                throw;
            }
        }

        public override async Task<User> UpdateAsync(User user)
        {
            try
            {
                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    _logger.LogWarning("Failed to update user {UserId}: {Errors}", user.Id, errors);
                    throw new UserUpdateException($"Failed to update user: {errors}");
                }
                return user;
            }
            catch (Exception ex) when (!(ex is UserUpdateException))
            {
                _logger.LogError(ex, "Error occurred while updating user {UserId}", user.Id);
                throw;
            }
        }

        public override async Task<bool> DeleteAsync(string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user == null)
                    return false;

                var result = await _userManager.DeleteAsync(user);
                return result.Succeeded;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting user {UserId}", id);
                throw;
            }
        }

        public async Task<bool> UpdateUserRoleAsync(string userId, Actors newRole)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                    return false;

                // Get current roles
                var currentRoles = await _userManager.GetRolesAsync(user);

                // Remove current roles
                if (currentRoles.Any())
                    await _userManager.RemoveFromRolesAsync(user, currentRoles);

                // Add new role
                var result = await _userManager.AddToRoleAsync(user, newRole.ToString());

                // Update user type
                user.UserType = newRole;
                await _userManager.UpdateAsync(user);

                return result.Succeeded;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating user role for {UserId}", userId);
                throw;
            }
        }
    }

}