using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillAssessmentPlatform.Core.Entities.TrackLevelStage
{
    class StageTrack
    {
        [Key]
        public int id { get; set; }
        public int trackid { get; set; }
        public string StageName { get; set; }

    }
}
