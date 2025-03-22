using SkillAssessmentPlatform.Core.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillAssessmentPlatform.Core.Entities.Tasks__Exams__and_Interviews
{
    public class ExamRequest
    {

        public int Id { get; set; }
        public int StageId { get; set; }
        public int ApplicantId { get; set; }
        public DateTime ScheduledDate { get; set; }
        public string Instructions { get; set; }
        public int FeedbackId { get; set; }

    }

}
}
