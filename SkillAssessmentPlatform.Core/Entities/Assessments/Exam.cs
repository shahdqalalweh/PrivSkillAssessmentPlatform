using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillAssessmentPlatform.Core.Entities.Tasks__Exams__and_Interviews
{
    public class Exam
    {
        public int Id { get; set; }
        public int StageId { get; set; }
        public int DurationMinutes { get; set; }
        public string Difficulty { get; set; }
        public string QuestionsType { get; set; }

        // Navigation properties
        public Stage Stage { get; set; }
        public ExamRequest ExamRequest { get; set; }
    }
}
