using SkillAssessmentPlatform.Core.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillAssessmentPlatform.Core.Interfaces
{
    public interface IUserRepository : IRepository<User>
    { 
        Task<User> GetUserByEmailAsync(string email);
        Task<IEnumerable<User>> SearchUsersAsync(string searchTerm, Actors userType);
        Task<bool> UpdateUserRoleAsync(string userId, Actors newRole);
    }
}
