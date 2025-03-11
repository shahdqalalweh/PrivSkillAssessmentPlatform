using Microsoft.AspNetCore.Identity;
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
        Task<string> RegisterApplicantAsync(User user, string password);
        Task<string> RegisterExaminerAsync(User user, string password);
        Task<string> EmailConfirmation(string email, string taken);
        Task<User>? LogInAsync(string email, string password);
        Task<string> ChangePasswordAsync(string email, string oldPassword, string newPassword);
        Task<string> ForgotPasswordAsync(string email);
        Task<string> ResetPassword(string email, string password, string token);
    }
}