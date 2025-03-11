using Microsoft.AspNetCore.Identity;
using SkillAssessmentPlatform.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillAssessmentPlatform.Core.Entities.Users
{
    public class Applicant : User
    {
        public ApplicantStatus Status { get; set; }

        public string? ExaminerID { get; set; }

        public Examiner Examiner { get; set; }
    }
}
