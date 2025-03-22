using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillAssessmentPlatform.Core.Entities.Certificates_and_Notifications
{
    public class Notification
    {

        public int Id { get; set; }
        public int UserId { get; set; }
        public string Message { get; set; }
        public string Type { get; set; }
        public DateTime Date { get; set; }
        public bool IsRead { get; set; }
    }
}
