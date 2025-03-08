using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using SkillAssessmentPlatform.Core.Entities.Users;
using SkillAssessmentPlatform.Core.Enums;
using SkillAssessmentPlatform.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillAssessmentPlatform.Infrastructure.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration configuration;

        public AuthRepository(UserManager<User> userManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            this.configuration = configuration;
        }

        
        public async Task<string> RegisterApplicantAsync(User user, string password)
        {
            var applicant = new Applicant
            {
                Email = user.Email,
                UserName = user.Email,
                UserType = Actors.Applicant,
                FullName = user.FullName,
                Status = ApplicantStatus.Inactive,
            };
            var result = await _userManager.CreateAsync(applicant, password);

            if (!result.Succeeded)
            {
                var errorMasseges = result.Errors.Select(e => e.Description).ToList();
                return string.Join(", ", errorMasseges);
            }
            return "1";
        }

        public async Task<string> RegisterExaminerAsync(User user, string password)
        {
            var examiner = new Examiner
            {
                Email = user.Email,
                UserName = user.Email,
                UserType = Actors.Examiner,
                FullName = user.FullName,
                Specialization = "----",
                MaxWorkLoad = 9,
                CurrWorkLoad = 0
            };
            var result = await _userManager.CreateAsync(examiner, password);

            if (!result.Succeeded)
            {
                var errorMasseges = result.Errors.Select(e => e.Description).ToList();
                return string.Join(", ", errorMasseges);
            }
            return "1";

        }
        public Task<string> ChangePasswordAsync(string email, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public async Task<User>? LogInAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if ( user == null)
            {
                return null;
            }
            if (!(await _userManager.CheckPasswordAsync(user, password)))
            {
                return null;
            }
            return user;

        }

    }
}
