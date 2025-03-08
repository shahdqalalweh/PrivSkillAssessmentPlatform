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
    public class ApplicantRepository :IApplicantRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly AppDbContext _dbContext;
        private readonly ILogger<ApplicantRepository> _logger;

        public ApplicantRepository(UserManager<User> userManager, 
            AppDbContext dbContext,
            ILogger<ApplicantRepository> logger)
           { 
            _userManager = userManager;
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<IEnumerable<Applicant>> GetAllApplicantsAsync()
        {
            return await _userManager.Users
                .OfType<Applicant>()
                .ToListAsync();
        }

        public async Task<Applicant> GetApplicantByIdAsync(string id)
        {
            return await _userManager.Users
                .OfType<Applicant>()
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task CreateApplicantAsync(Applicant applicant)
        {
            await _userManager.CreateAsync(applicant);
        }

        public async Task UpdateApplicantAsync(Applicant applicant)
        {
            await _userManager.UpdateAsync(applicant);
        }

        public async Task DeleteApplicantAsync(string id)
        {
            var applicant = await _userManager.FindByIdAsync(id);
            if (applicant != null)
            {
                await _userManager.DeleteAsync(applicant);
            }
        }

        public ApplicantStatus GetApplicantStatus()
        {
            throw new NotImplementedException();
        }

        public Task<User>? GetUserByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAllUsersAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateUserAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteUserAsync(string id)
        {
            throw new NotImplementedException();
        }

        //public async Task EnrollApplicantInTrackAsync(string applicantId, int trackId)
        //{
        //    var applicant = await _userManager.FindByIdAsync(applicantId) as Applicant;
        //    if (applicant != null)
        //    {
        //        applicant.TrackId = trackId;
        //        await _userManager.UpdateAsync(applicant);
        //    }
        //}
    }
}
