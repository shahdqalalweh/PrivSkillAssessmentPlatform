using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillAssessmentPlatform.API.Common;
using SkillAssessmentPlatform.Application.DTOs;
using SkillAssessmentPlatform.Application.Services;
using SkillAssessmentPlatform.Core.Exceptions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SkillAssessmentPlatform.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TracksController : ControllerBase
    {
        private readonly TrackService _trackService;
        private readonly ExaminerService _examinerService;
        private readonly IResponseHandler _responseHandler;

        public TracksController(TrackService trackService, ExaminerService examinerService, IResponseHandler responseHandler)
        {
            _trackService = trackService;
            _examinerService = examinerService;
            _responseHandler = responseHandler;
        }

        // GET: api/TrackApi
        [HttpGet]
        public async Task<IActionResult> GetTracks()
        {
            var tracks = await _trackService.GetAllTracksAsync();
            return _responseHandler.Success(tracks);
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
                // return _responseHandler.BadRequest("bad request");
                throw new BadRequestException("IDes do not match");

            var success = await _trackService.UpdateTrackAsync(id, trackDto);
            if (!success)
                throw new KeyNotFoundException();

            return _responseHandler.Success<string>("upadated");
        }


        // POST: api/TrackApi
        [HttpPost]
       // [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PostTrack([FromBody] TrackDto trackDto)
        {
            var createdTrack = await _trackService.CreateTrackAsync(trackDto);
            return _responseHandler.Created(createdTrack);
            //return CreatedAtAction(nameof(GetTrack), new { id = createdTrack.Id }, createdTrack);
        }

        // DELETE: api/TrackApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrack(int id)
        {
            var success = await _trackService.DeleteTrackAsync(id);
            if (!success)
                return NotFound();
            return _responseHandler.Success();
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
    }
}
       
