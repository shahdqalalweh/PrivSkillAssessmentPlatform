using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillAssessmentPlatform.Core.Exceptions
{
    public class SendEmailException : Exception
    {
        public SendEmailException() { }

        public SendEmailException(string message) : base(message) { }

        public SendEmailException(string message, Exception innerException) : base(message, innerException) { }
    }
}

