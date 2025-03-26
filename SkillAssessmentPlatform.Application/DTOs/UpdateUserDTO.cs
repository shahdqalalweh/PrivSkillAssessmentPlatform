using SkillAssessmentPlatform.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillAssessmentPlatform.Application.DTOs
{
    public class UpdateUserDTO
    {
        [Required]
        [MaxLength(100)]
        public string FullName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public Gender? Gender { get; set; }
    }
}
