using SkillAssessmentPlatform.Core.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillAssessmentPlatform.Core.Entities.Certificates_and_Notifications
{
    public class Certificate
    {

        public int Id { get; set; }
        public string ApplicantId { get; set; }
        public int LeveProgressId { get; set; }
        public DateTime IssueDate { get; set; }
        public string VerificationCode { get; set; }
        // Navigation properties
        public Applicant Applicant { get; set; }
        public LevelProgress LevelProgress { get; set; }
    }
}
