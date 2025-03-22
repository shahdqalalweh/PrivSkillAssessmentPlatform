using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillAssessmentPlatform.Core.Entities
{
    public class LevelProgress
    {
        public int Id { get; set; }
        public int EnrollmentId { get; set; }
        public int LevelId { get; set; }
        public string Status { get; set; } 
        public DateTime StartDate { get; set; }
        public DateTime CompletionDate { get; set; }
        public Level Level { get; set; } // there is a relation bett levelprog & Level
    }
}
