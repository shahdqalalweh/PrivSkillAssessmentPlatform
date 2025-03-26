using SkillAssessmentPlatform.Core.Entities.Feedback_and_Evaluation;
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
        public int ExamId { get; set; }
        public int ApplicantId { get; set; }
        public DateTime ScheduledDate { get; set; }
        public string Instructions { get; set; }
        public int FeedbackId { get; set; }

        // Navigation properties
        public Exam Exam { get; set; }
        public Feedback Feedback { get; set; } = new Feedback();

    }

}

