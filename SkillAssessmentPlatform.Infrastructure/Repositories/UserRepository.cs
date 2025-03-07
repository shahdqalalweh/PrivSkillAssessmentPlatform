using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SkillAssessmentPlatform.Core.Entities.Users;
using SkillAssessmentPlatform.Core.Enums;
using SkillAssessmentPlatform.Core.Interfaces;
using SkillAssessmentPlatform.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillAssessmentPlatform.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly IApplicantRepository _applicantRepository;
        private readonly IExaminerRepository _examinerRepository;
        private readonly ILogger<UserRepository> _logger;
        //private readonly AppDbContext _dbContext;

        public UserRepository(UserManager<User> userManager,
             IApplicantRepository applicantRepository,
               IExaminerRepository examinerRepository,
               ILogger<UserRepository> logger)
        {
            _userManager = userManager;
            _applicantRepository = applicantRepository;
            _examinerRepository = examinerRepository;
            _logger = logger;
        }

        public async Task<string> RegisterApplicantAsync(User user, string password)
        {
            _logger.LogInformation("AuthService-RegisterApplicant");
            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                _logger.LogInformation("Add user succeeded");
            }
            
            if (!result.Succeeded)
            {
                var errorMasseges = result.Errors.Select(e => e.Description).ToList();
                return string.Join(", ", errorMasseges);
            }
            await _userManager.AddToRoleAsync(user, Actors.Applicant);
            var applicant = new Applicant
            {
                Id = user.Id,
                Status = ApplicantStatus.Inactive,
                ExaminerID = null,
                User = user,
            };
            _logger.LogInformation("applicant created " + user.FullName);


            _applicantRepository.Add(applicant);
            return " Succeeded";

        }

        public async Task<string> RegisterExaminerAsync(User user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
            {
                var errorMasseges = result.Errors.Select(e => e.Description).ToList();
                return string.Join(", ", errorMasseges);
            }
            var examiner = new Examiner
            {
                Id = user.Id,
                Specialization = string.Empty,
                TrackID = null,
                MaxWorkLoad = 0,
                CurrWorkLoad = 0
            };

            _examinerRepository.Add(examiner);
            return " Succeeded";

        }

        public async Task<User>? GetUserByIdAsync(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            await _userManager.UpdateAsync(user);
        }

        public async Task DeleteUserAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }
        }




    }
}