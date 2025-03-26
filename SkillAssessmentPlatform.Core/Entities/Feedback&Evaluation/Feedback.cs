using SkillAssessmentPlatform.Core.Entities.Tasks__Exams__and_Interviews;
using SkillAssessmentPlatform.Core.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillAssessmentPlatform.Core.Entities.Feedback_and_Evaluation
{
    public class Feedback
    {
        public int Id { get; set; }
        public string ExaminerId { get; set; }
        public string Comments { get; set; }
        public decimal TotalScore { get; set; }
        public DateTime FeedbackDate { get; set; }

        // Navigation properties
        public Examiner Examiner { get; set; }
        public ICollection<DetailedFeedback> DetailedFeedbacks { get; set; }
        public TaskSubmission TaskSubmission { get; set; }
        public InterviewBook InterviewBook { get; set; }
        public ExamRequest ExamRequest { get; set; }


    }
}
