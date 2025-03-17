
 using Microsoft.AspNetCore.Mvc;
    using SkillAssessmentPlatform.API.Common;
    using SkillAssessmentPlatform.Core.Common;
    using System.Collections.Generic;
    using System.Net;

namespace SkillAssessmentPlatform.API.Bases
{

 
    public class ResponseHandler : IResponseHandler
    {
        public IActionResult Success<T>(T entity, object meta = null)
        {
            return new OkObjectResult(new Response<T>(entity, "Success", HttpStatusCode.OK) { Meta = meta });
        }
        public IActionResult Created<T>(T entity, object meta = null)
        {
            return new ObjectResult(new Response<T>(entity, "Created Successfully", HttpStatusCode.Created) { Meta = meta })
            {
                StatusCode = (int)HttpStatusCode.Created
            };
        }
        public IActionResult Deleted()
        {
            return new OkObjectResult(new Response<string>(null, "Deleted Successfully", HttpStatusCode.OK));
        }
        public IActionResult Unauthorized()
        {
            return new UnauthorizedObjectResult(new Response<string>(null, "Unauthorized", HttpStatusCode.Unauthorized));
        }
        public IActionResult BadRequest(string message = "Bad Request", List<string> errors = null)
        {
            return new BadRequestObjectResult(new Response<string>(message, HttpStatusCode.BadRequest) { Errors = errors });
        }
        public IActionResult BadRequest(string message = "Bad Request", HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest, List<string> errors = null)
        {
            return new BadRequestObjectResult(new Response<string>(message, httpStatusCode) { Errors = errors });
        }
        public IActionResult NotFound(string message = "Not Found")
        {
            return new NotFoundObjectResult(new Response<string>(message, HttpStatusCode.NotFound));
        }
    }

}