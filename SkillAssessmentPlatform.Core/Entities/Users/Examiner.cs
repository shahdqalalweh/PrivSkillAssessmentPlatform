using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillAssessmentPlatform.Core.Entities.Users
{
    public class Examiner :User
    {
        public string Specialization {  get; set; }

        public ICollection<ExaminerLoad> ExaminerLoads { get; set; }

    }
}
