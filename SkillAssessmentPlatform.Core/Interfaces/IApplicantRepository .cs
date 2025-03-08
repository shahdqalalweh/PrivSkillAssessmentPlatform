using SkillAssessmentPlatform.Core.Entities.Users;
using SkillAssessmentPlatform.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillAssessmentPlatform.Core.Interfaces
{
    public interface IApplicantRepository 
    {
        Task<IEnumerable<Applicant>> GetAllApplicantsAsync();
        Task<Applicant> GetApplicantByIdAsync(string id);
        Task CreateApplicantAsync(Applicant applicant);
        Task UpdateApplicantAsync(Applicant applicant);
        Task DeleteApplicantAsync(string id);
        //Task EnrollApplicantInTrackAsync(string applicantId, int trackId);
        ApplicantStatus GetApplicantStatus();
    }
}
