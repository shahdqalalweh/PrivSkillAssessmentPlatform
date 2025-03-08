using SkillAssessmentPlatform.Core.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillAssessmentPlatform.Core.Interfaces
{
    public interface IExaminerRepository 
    {
        Task<IEnumerable<Examiner>> GetAllExaminersAsync();
        Task<Examiner>? GetExaminerByIdAsync(string id);
        Task CreateExaminerAsync(Examiner examiner);
        Task UpdateExaminerAsync(Examiner examiner);
        Task DeleteExaminerAsync(string id);
        //Task<IEnumerable<Examiner>> GetExaminersByTrackAsync(int trackId);
        int GetMaxWorkLoad(String id);
        int GetCurrWorkLoad(String id);

    }
}
