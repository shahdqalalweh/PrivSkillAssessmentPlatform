using SkillAssessmentPlatform.Core.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillAssessmentPlatform.Core.Interfaces
{
    public interface IUserRepository 
    {
        Task<string> RegisterApplicantAsync(User user, string password);
        Task<string> RegisterExaminerAsync(User user, string password);
        Task<User>? GetUserByIdAsync(string id);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(string id);


    }
}
