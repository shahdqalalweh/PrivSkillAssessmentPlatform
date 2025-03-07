using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SkillAssessmentPlatform.Application.Services;
using SkillAssessmentPlatform.Core.Entities.Users;

namespace SkillAssessmentPlatform.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ExaminerController : ControllerBase
    {
        private readonly ExaminerService _examinerService;

        public ExaminerController(ExaminerService examinerService)
        {
            _examinerService = examinerService;
        }

        //[HttpGet]
        //public async Task<IActionResult> GetAllExaminers()
        //{
        //    var examiners = await _examinerService.GetAllExaminersAsync();
        //    return Ok(examiners);
        //}

        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetExaminerById(string id)
        //{
        //    var examiner = await _examinerService.GetExaminerByIdAsync(id);
        //    if (examiner == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(examiner);
        //}

        //[HttpPost]
        //public async Task<IActionResult> CreateExaminer(Examiner examiner)
        //{
        //    await _examinerService.CreateExaminerAsync(examiner);
        //    return CreatedAtAction(nameof(GetExaminerById), new { id = examiner.Id }, examiner);
        //}

        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateExaminer(string id, Examiner examiner)
        //{
        //    if (id != examiner.Id)
        //    {
        //        return BadRequest();
        //    }

        //    await _examinerService.UpdateExaminerAsync(examiner);
        //    return NoContent();
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteExaminer(string id)
        //{
        //    await _examinerService.DeleteExaminerAsync(id);
        //    return NoContent();
        //}

        //[HttpGet("track/{trackId}")]
        //public async Task<IActionResult> GetExaminersByTrack(int trackId)
        //{
        //    var examiners = await _examinerService.GetExaminersByTrackAsync(trackId);
        //    return Ok(examiners);
        //}
    }
}