using Microsoft.AspNetCore.Identity;
using SkillAssessmentPlatform.Core.Common;
using SkillAssessmentPlatform.Core.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillAssessmentPlatform.Core.Interfaces
{
    public interface IAuthRepository
    {
        Task<Applicant> RegisterApplicantAsync(User user, string password);
        Task<Examiner> RegisterExaminerAsync(User user, string password);
        Task<bool> SendEmailAsync(string email, string token, string endpoint, string subject, string action, string message);
        Task EmailConfirmation(string email, string taken);
        Task<User>? LogInAsync(string email, string password);
        Task ChangePasswordAsync(string email, string oldPassword, string newPassword);
        Task ForgotPasswordAsync(string email);
        Task ResetPassword(string email, string password, string token);
        Task DeleteUserAsync(string id);
        Task<bool> UpdateUserEmail(string userId, string newEmail);
    }
}