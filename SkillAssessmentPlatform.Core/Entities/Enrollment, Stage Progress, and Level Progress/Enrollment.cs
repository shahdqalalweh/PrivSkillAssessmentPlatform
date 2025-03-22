using SkillAssessmentPlatform.Core.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillAssessmentPlatform.Core.Entities
{
    public class Enrollment
    {
        public int Id { get; set; }
        public int ApplicantId { get; set; }
        public int TrackId { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public string Status { get; set; }  
        public Applicant Applicant { get; set; }
        public Track Track { get; set; }
        public List<LevelProgress> LevelProgressRecords { get; set; } = new();
    }
}
