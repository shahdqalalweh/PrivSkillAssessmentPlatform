using Microsoft.AspNetCore.Identity;
using SkillAssessmentPlatform.Core.Entities.Certificates_and_Notifications;
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

        public ICollection<Enrollment> Enrollments { get; set; }
        public ICollection<Certificate> Certificates { get; set; }
    }
}
