using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillAssessmentPlatform.Core.Entities
{
    public class Stage
    {
        public int Id { get; set; }
        public int LevelId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }  // "Exam", "Task", "Interview"
        public int Order { get; set; }
        public bool IsActive { get; set; }
        public int PassingScore { get; set; }
        public Level Level { get; set; }
        public List<StageProgress> StageProgressRecords { get; set; } = new();
    }
}
