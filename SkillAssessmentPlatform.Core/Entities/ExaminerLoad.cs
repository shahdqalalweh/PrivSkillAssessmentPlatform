using SkillAssessmentPlatform.Core.Entities.Users;
using SkillAssessmentPlatform.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillAssessmentPlatform.Core.Entities
{
    public class ExaminerLoad
    {
        public int ID { get; set; }

        public string ExaminerID { get; set; }
        public StageType Type { get; set; }

        public int MaxWorkLoad { get; set; }

        public int CurrWorkLoad { get; set; } = 0;


        // Navigation properties
        public Examiner Examiner { get; set; }
    }
}
