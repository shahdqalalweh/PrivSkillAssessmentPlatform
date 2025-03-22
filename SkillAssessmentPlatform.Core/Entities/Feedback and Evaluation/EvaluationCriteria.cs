using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillAssessmentPlatform.Core.Entities.Feedback_and_Evaluation
{
    public class EvaluationCriteria
    {

        public int Id { get; set; }
        public int StageId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Weight { get; set; }
    }
}
