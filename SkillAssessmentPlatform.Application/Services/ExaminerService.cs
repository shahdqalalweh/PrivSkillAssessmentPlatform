using SkillAssessmentPlatform.Core.Entities.Users;
using SkillAssessmentPlatform.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillAssessmentPlatform.Application.Services
{
        public class ExaminerService
        {
            private readonly IExaminerRepository _examinerRepository;

            public ExaminerService(IExaminerRepository examinerRepository)
            {
                _examinerRepository = examinerRepository;
            }

            public async Task<IEnumerable<Examiner>> GetAllExaminersAsync()
            {
                return await _examinerRepository.GetAllExaminersAsync();
            }

            public async Task<Examiner> GetExaminerByIdAsync(string id)
            {
                return await _examinerRepository.GetExaminerByIdAsync(id);
            }

            public async Task CreateExaminerAsync(Examiner examiner)
            {
                await _examinerRepository.CreateExaminerAsync(examiner);
            }

            public async Task UpdateExaminerAsync(Examiner examiner)
            {
                await _examinerRepository.UpdateExaminerAsync(examiner);
            }

            public async Task DeleteExaminerAsync(string id)
            {
                await _examinerRepository.DeleteExaminerAsync(id);
            }

            //public async Task<IEnumerable<Examiner>> GetExaminersByTrackAsync(int trackId)
            //{
            //    return await _examinerRepository.GetExaminersByTrackAsync(trackId);
            //}
        }
    
}
