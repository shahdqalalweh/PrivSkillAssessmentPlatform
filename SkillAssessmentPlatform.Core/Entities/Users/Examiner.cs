using SkillAssessmentPlatform.Core.Entities.Feedback_and_Evaluation;
using SkillAssessmentPlatform.Core.Entities.Tasks__Exams__and_Interviews;
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

        // Navigation properties
        public User User { get; set; }
        public ICollection<ExaminerLoad> ExaminerLoads { get; set; }
        public ICollection<Track> ManagedTracks { get; set; }
        public ICollection<Track> WorkingTracks { get; set; }
        public ICollection<StageProgress> SupervisedStages { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
        public ICollection<Feedback> Feedbacks { get; set; }
    }

}

