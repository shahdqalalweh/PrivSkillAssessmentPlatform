using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SkillAssessmentPlatform.Application.Services;
using SkillAssessmentPlatform.Core.Entities.Users;

namespace SkillAssessmentPlatform.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicantController : ControllerBase
    {
       // private readonly ApplicantService _applicantService;

     ///   public ApplicantController(ApplicantService applicantService)
     //   {
       //     _applicantService = applicantService;
      //  }

        //[HttpGet]
        //public async Task<IActionResult> GetAllApplicants()
        //{
        //    var applicants = await _applicantService.GetAllApplicantsAsync();
        //    return Ok(applicants);
        //}

        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetApplicantById(string id)
        //{
        //    var applicant = await _applicantService.GetApplicantByIdAsync(id);
        //    if (applicant == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(applicant);
        //}

        //[HttpPost]
        //public async Task<IActionResult> CreateApplicant(Applicant applicant)
        //{
        //    await _applicantService.CreateApplicantAsync(applicant);
        //    return CreatedAtAction(nameof(GetApplicantById), new { id = applicant.Id }, applicant);
        //}

        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateApplicant(string id, Applicant applicant)
        //{
        //    if (id != applicant.Id)
        //    {
        //        return BadRequest();
        //    }

        //    await _applicantService.UpdateApplicantAsync(applicant);
        //    return NoContent();
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteApplicant(string id)
        //{
        //    await _applicantService.DeleteApplicantAsync(id);
        //    return NoContent();
        //}

        //[HttpPost("{applicantId}/enroll/{trackId}")]
        //public async Task<IActionResult> EnrollApplicantInTrack(string applicantId, int trackId)
        //{
        //    await _applicantService.EnrollApplicantInTrackAsync(applicantId, trackId);
        //    return Ok();
        //}
    }
}