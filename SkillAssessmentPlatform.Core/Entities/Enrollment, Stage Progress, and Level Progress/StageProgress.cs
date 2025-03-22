using SkillAssessmentPlatform.Core.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillAssessmentPlatform.Core.Entities
{
    public class StageProgress
    {
        public int Id { get; set; }
        public int EnrollmentId { get; set; }
        public int StageId { get; set; }
        public string Status { get; set; }  
        public int Score { get; set; }
        public int ExaminerId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime CompletionDate { get; set; }
        public int Attempts { get; set; }
        // add by the relation bett stageProg && EXaminer && Stage
        public Examiner Examiner { get; set; }
        public Stage Stage { get; set; }
    }
}
