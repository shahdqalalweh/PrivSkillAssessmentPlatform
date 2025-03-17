using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace SkillAssessmentPlatform.API.Common
{
    public interface IResponseHandler
    {
        IActionResult Success<T>(T entity, object meta = null);
        IActionResult Created<T>(T entity, object meta = null);
        IActionResult Deleted();
        IActionResult Unauthorized();
        IActionResult BadRequest(string message = "Bad Request", List<string> errors = null);
        IActionResult BadRequest(string message = "Bad Request", HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest, List<string> errors = null);
        IActionResult NotFound(string message = "Not Found");
    }
}
