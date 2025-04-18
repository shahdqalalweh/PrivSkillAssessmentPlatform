﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillAssessmentPlatform.Application.DTOs
{
    public class StageDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; } // or use Enum
        public int Order { get; set; }
        public double PassingScore { get; set; }
        public List<EvaluationCriteriaDTO> EvaluationCriteria { get; set; }
    }

}
