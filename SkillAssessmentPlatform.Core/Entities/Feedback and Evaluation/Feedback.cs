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
        public int ExaminerId { get; set; }
        public string Comments { get; set; }
        public decimal TotalScore { get; set; }
        public DateTime FeedbackDate { get; set; }
        public List<DetailedFeedback> DetailedFeedbacks { get; set; } = new();


    }
}
