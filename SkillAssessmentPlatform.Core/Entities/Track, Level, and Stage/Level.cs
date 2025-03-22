using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillAssessmentPlatform.Core.Entities
{
    public class Level
    {
        public int Id { get; set; }
        public int TrackId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
        public bool IsActive { get; set; }
        public Track Track { get; set; }
        public List<Stage> Stages { get; set; } = new();
    }
}
