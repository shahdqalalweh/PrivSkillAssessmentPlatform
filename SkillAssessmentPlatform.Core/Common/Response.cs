using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SkillAssessmentPlatform.Core.Common
{
    public class Response<T>
    {
        public Response() { }

        public Response(T data, string message = null, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            Succeeded = true;
            Message = message;
            Data = data;
            StatusCode = statusCode;
        }

        public Response(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            Succeeded = (statusCode == HttpStatusCode.OK || statusCode == HttpStatusCode.Created)? true : false;
            Message = message;
            StatusCode = statusCode;
            Errors = new List<string>();
        }

        public Response(string message, HttpStatusCode statusCode, List<string> errors)
        {
            Succeeded = false;
            Message = message;
            StatusCode = statusCode;
            Errors = errors ?? new List<string>();
        }

        public HttpStatusCode StatusCode { get; set; }
        public object Meta { get; set; }
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
        public T Data { get; set; }
    }

}
