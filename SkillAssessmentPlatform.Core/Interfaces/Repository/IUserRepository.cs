using SkillAssessmentPlatform.Core.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillAssessmentPlatform.Core.Interfaces.Repository
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetUserByEmailAsync(string email);
        Task<IEnumerable<User>> SearchUsersAsync(string searchTerm, Actors userType);
        Task<bool> UpdateUserRoleAsync(string userId, Actors newRole);
        Task<IEnumerable<User>> GetUsersByTypeAsync(Actors userType, int page = 1, int pageSize = 10);
        Task<int> GetCountByTypeAsync(Actors userType);
    }
}
