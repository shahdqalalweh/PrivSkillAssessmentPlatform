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