using Microsoft.AspNetCore.Mvc;
using SkillAssessmentPlatform.Application.DTOs;
using SkillAssessmentPlatform.Application.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SkillAssessmentPlatform.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrackApiController : ControllerBase
    {
        private readonly TrackService _trackService;

        public TrackApiController(TrackService trackService)
        {
            _trackService = trackService;
        }

        // GET: api/TrackApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrackDto>>> GetTracks()
        {
            var tracks = await _trackService.GetAllTracksAsync();
            return Ok(tracks);
        }

        // GET: api/TrackApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TrackDto>> GetTrack(int id)
        {
            var track = await _trackService.GetTrackByIdAsync(id);
            if (track == null)
                return NotFound();
            return Ok(track);
        }

        // PUT: api/TrackApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrack(int id, [FromBody] TrackDto trackDto)
        {
            if (id != trackDto.Id)
                return BadRequest();

            var success = await _trackService.UpdateTrackAsync(id, trackDto);
            if (!success)
                return NotFound();

            return NoContent();
        }

        // POST: api/TrackApi
        [HttpPost]
        public async Task<ActionResult<TrackDto>> PostTrack([FromBody] TrackDto trackDto)
        {
            var createdTrack = await _trackService.CreateTrackAsync(trackDto);
            return CreatedAtAction(nameof(GetTrack), new { id = createdTrack.Id }, createdTrack);
        }

        // DELETE: api/TrackApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrack(int id)
        {
            var success = await _trackService.DeleteTrackAsync(id);
            if (!success)
                return NotFound();
            return NoContent();
        }

        // GET: api/TrackApi/5/levels
        [HttpGet("{id}/levels")]
        public async Task<IActionResult> GetLevelsByTrack(int id)
        {
            var levels = await _trackService.GetLevelsByTrackIdAsync(id);
            return Ok(levels);
        }

        // POST: api/TrackApi/{trackId}/levels
        [HttpPost("{trackId}/levels")]
        public async Task<IActionResult> PostLevel(int trackId, [FromBody] LevelDto levelDto)
        {
            var createdLevel = await _trackService.CreateLevelAsync(trackId, levelDto);
            return CreatedAtAction(nameof(GetLevelsByTrack), new { id = trackId }, createdLevel);
        }

        // POST: api/TrackApi/{id}/examiners
        [HttpPost("{id}/examiners")]
        public async Task<IActionResult> AssignExaminer(int id, [FromBody] ExaminerAssignmentDto assignmentDto)
        {
            var result = await _trackService.AssignExaminerAsync(id, assignmentDto);
            if (!result)
                return BadRequest();
            return Ok();
        }

        // DELETE: api/TrackApi/{id}/examiners/{examinerId}
        [HttpDelete("{id}/examiners/{examinerId}")]
        public async Task<IActionResult> RemoveExaminer(int id, string examinerId)
        {
            var result = await _trackService.RemoveExaminerAsync(id, examinerId);
            if (!result)
                return NotFound();
            return NoContent();
        }
    }
}
