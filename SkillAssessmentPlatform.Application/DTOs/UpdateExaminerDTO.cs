using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillAssessmentPlatform.Application.DTOs
{
    public class UpdateExaminerDTO : UpdateUserDTO
    {
        [Required]
        [MaxLength(100)]
        public string Specialization { get; set; }
    }
}
