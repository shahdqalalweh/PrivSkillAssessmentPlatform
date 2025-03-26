using SkillAssessmentPlatform.Core.Entities.Feedback_and_Evaluation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillAssessmentPlatform.Core.Entities.Tasks__Exams__and_Interviews
{
    public class InterviewBook
    {

        public int Id { get; set; }
        public int InterviewId { get; set; }
        public int AppointmentId { get; set; }
        public int ApplicantId { get; set; }
        public DateTime ScheduledDate { get; set; }
        public string MeetingLink { get; set; }
        public string Status { get; set; }
        public int FeedbackId { get; set; }
   
        // Navigation properties
        public Interview Interview { get; set; }
        public Appointment Appointment { get; set; }
        public Feedback Feedback { get; set; }

    }
}
