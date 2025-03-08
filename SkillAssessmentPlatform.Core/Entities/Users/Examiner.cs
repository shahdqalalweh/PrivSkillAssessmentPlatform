using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillAssessmentPlatform.Core.Entities.Users
{
    public class Examiner :User
    {
        public string Specialization {  get; set; }
        public int? TrackID { get; set; }
        public int MaxWorkLoad { get; set; }
        public int CurrWorkLoad { get; set; } = 0;

    }
}
