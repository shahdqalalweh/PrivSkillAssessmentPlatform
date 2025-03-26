using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillAssessmentPlatform.Core.Exceptions
{
    public class BadRequestException : Exception
    {
        public List<string> Errors { get; } = new List<string>();

        // لأخطاء بسيطة برسالة واحدة
        public BadRequestException(string message)
            : base(message)
        {
            Errors = new List<string> { message };
        }

        // لأخطاء متعددة
        public BadRequestException(string message, IEnumerable<string> errors)
            : base(message)
        {
            Errors = new List<string>(errors);
        }

        // لأخطاء IdentityResult وغيرها
        public BadRequestException(string message, IEnumerable<IdentityError> errors)
            : base(message)
        {
            Errors = errors.Select(e => e.Description).ToList();
        }
    }
}
