using Microsoft.AspNetCore.Identity;
using SkillAssessmentPlatform.Core.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillAssessmentPlatform.Core.Entities.Users
{
     public class User : IdentityUser
    {
        public string FullName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public Actors UserType { get; set; }
        public string? Image {  get; set; }
        public Gendar? Gendar { get; set; }
    }
}
