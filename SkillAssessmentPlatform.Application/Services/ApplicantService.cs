using SkillAssessmentPlatform.Core.Entities.Users;
using SkillAssessmentPlatform.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillAssessmentPlatform.Application.Services
{
        public class ApplicantService
        {
            private readonly IApplicantRepository _applicantRepository;

            public ApplicantService(IApplicantRepository applicantRepository)
            {
                _applicantRepository = applicantRepository;
            }

            public async Task<IEnumerable<Applicant>> GetAllApplicantsAsync()
            {
                return await _applicantRepository.GetAllApplicantsAsync();
            }

            public async Task<Applicant> GetApplicantByIdAsync(string id)
            {
                return await _applicantRepository.GetApplicantByIdAsync(id);
            }

            public async Task CreateApplicantAsync(Applicant applicant)
            {
                await _applicantRepository.CreateApplicantAsync(applicant);
            }

            public async Task UpdateApplicantAsync(Applicant applicant)
            {
                await _applicantRepository.UpdateApplicantAsync(applicant);
            }

            public async Task DeleteApplicantAsync(string id)
            {
                await _applicantRepository.DeleteApplicantAsync(id);
            }

            //public async Task EnrollApplicantInTrackAsync(string applicantId, int trackId)
            //{
            //    await _applicantRepository.EnrollApplicantInTrackAsync(applicantId, trackId);
            //}
        }
   
}
