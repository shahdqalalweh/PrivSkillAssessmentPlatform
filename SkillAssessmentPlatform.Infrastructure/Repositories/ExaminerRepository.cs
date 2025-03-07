using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
    public class ExaminerRepository : IExaminerRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly AppDbContext _context;

        public ExaminerRepository(UserManager<User> userManager, AppDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        public void Add(Examiner examiner)
        {
            _context.Examiners.Add(examiner);
            _context.SaveChanges();
        }
        public async Task<IEnumerable<Examiner>> GetAllExaminersAsync()
        {
            return await _userManager.Users
                .OfType<Examiner>()
                .ToListAsync();
        }

        public async Task<Examiner>? GetExaminerByIdAsync(string id)
        {
            return await _userManager.Users
                .OfType<Examiner>()
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task CreateExaminerAsync(Examiner examiner)
        {
            await _userManager.CreateAsync(examiner);
        }

        public async Task UpdateExaminerAsync(Examiner examiner)
        {
            await _userManager.UpdateAsync(examiner);
        }

        public async Task DeleteExaminerAsync(string id)
        {
            var examiner = await _userManager.FindByIdAsync(id);
            if (examiner != null)
            {
                await _userManager.DeleteAsync(examiner);
            }
        }

        public int GetMaxWorkLoad(string id)
        {
            throw new NotImplementedException();
        }

        public int GetCurrWorkLoad(string id)
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

        //public async Task<IEnumerable<Examiner>> GetExaminersByTrackAsync(int trackId)
        //{
        //    return await _userManager.Users
        //        .OfType<Examiner>()
        //        .Where(e => e.TrackId == trackId)
        //        .ToListAsync();
        //}
    }
}
