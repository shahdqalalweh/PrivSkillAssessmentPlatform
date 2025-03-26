using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillAssessmentPlatform.Application.DTOs
{
    public class ExaminerDTO : UserDTO
    {
        public string Specialization { get; set; }
        public IEnumerable<ExaminerLoadDTO> ExaminerLoads { get; set; }
    }
}
