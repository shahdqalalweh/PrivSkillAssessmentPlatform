using SkillAssessmentPlatform.Core.Entities;
using SkillAssessmentPlatform.Core.Entities.Feedback_and_Evaluation;
using SkillAssessmentPlatform.Application.DTOs;




[ApiController]
[Route("api/[controller]")]
public class TracksController : ControllerBase
{
    private readonly TrackService _trackService;
    private readonly IResponseHandler _responseHandler;

    public TracksController(TrackService trackService, IResponseHandler responseHandler)
    {
        _trackService = trackService;
        _responseHandler = responseHandler;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _trackService.GetAllTracksAsync();
        return _responseHandler.Success(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var track = await _trackService.GetTrackByIdAsync(id);
        return _responseHandler.Success(track);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTrackDTO dto)
    {
        var created = await _trackService.CreateTrackAsync(dto);
        return _responseHandler.Created(created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateTrackDTO dto)
    {
        var updated = await _trackService.UpdateTrackAsync(id, dto);
        return _responseHandler.Success(updated, "Track updated successfully");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _trackService.DeleteTrackAsync(id);
        return _responseHandler.Deleted();
    }

    [HttpGet("{id}/levels")]
    public async Task<IActionResult> GetLevels(int id)
    {
        var levels = await _trackService.GetLevelsByTrackAsync(id);
        return _responseHandler.Success(levels);
    }

    [HttpPost("{trackId}/levels")]
    public async Task<IActionResult> CreateLevel(int trackId, [FromBody] CreateLevelDTO dto)
    {
        var level = await _trackService.CreateLevelAsync(trackId, dto);
        return _responseHandler.Created(level);
    }

    [HttpPost("{id}/examiners")]
    public async Task<IActionResult> AssignExaminer(int id, [FromQuery] string examinerId)
    {
        await _trackService.AssignExaminerAsync(id, examinerId);
        return _responseHandler.Success("Examiner assigned to track");
    }

    [HttpDelete("{id}/examiners/{examinerId}")]
    public async Task<IActionResult> RemoveExaminer(int id, string examinerId)
    {
        await _trackService.RemoveExaminerAsync(id, examinerId);
        return _responseHandler.Success("Examiner removed from track");
    }

    [HttpGet("{id}/stages/count")]
    public async Task<IActionResult> GetStageCount(int id)
    {
        var count = await _trackService.GetStageCountAsync(id);
        return _responseHandler.Success(count);
    }

    [HttpGet("{id}/structure")]
    public async Task<IActionResult> GetLevelsAndStages(int id)
    {
        var structure = await _trackService.GetLevelsWithStagesAsync(id);
        return _responseHandler.Success(structure);
    }

    [HttpPost("{id}/structure-with-criteria")]
    public async Task<IActionResult> GetStructureWithCriteria(int id)
    {
        var result = await _trackService.GetLevelsStagesCriteriaAsync(id);
        return _responseHandler.Success(result);
    }
}