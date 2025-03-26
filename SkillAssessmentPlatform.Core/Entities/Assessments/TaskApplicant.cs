using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillAssessmentPlatform.Core.Entities.Tasks__Exams__and_Interviews
{
    public class TaskApplicant
    {

        public int Id { get; set; }
        public int TaskId { get; set; }
        public int ApplicantId { get; set; }
        public DateTime AssignedDate { get; set; }
        public DateTime DueDate { get; set; }

        // Navigation properties
        public Task Task { get; set; }
        public StageProgress StageProgress { get; set; }
        public ICollection<TaskSubmission> TaskSubmissions { get; set; } = new List<TaskSubmission>();
    }
}
