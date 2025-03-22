using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillAssessmentPlatform.Core.Entities.Tasks__Exams__and_Interviews
{
    public class TaskSubmission
    {

        public int Id { get; set; }
        public int TaskApplicantId { get; set; }
        public string SubmissionUrl { get; set; }
        public DateTime SubmissionDate { get; set; }
        public int FeedbackId { get; set; }

    }
}
