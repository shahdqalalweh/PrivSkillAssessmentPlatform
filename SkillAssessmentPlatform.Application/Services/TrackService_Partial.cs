
using SkillAssessmentPlatform.Core.Entities;
using SkillAssessmentPlatform.Core.Entities.Feedback_and_Evaluation;
using SkillAssessmentPlatform.Application.DTOs;
using AutoMapper;



public partial class TrackService
{
    private readonly ITrackRepository _trackRepository;
    

    public async Task<Level> CreateLevelAsync(int trackId, CreateLevelDTO dto)
    {
        var track = await _trackRepository.GetTrackWithLevelsAsync(trackId);
        var level = new Level
        {
            Name = dto.Name,
            Description = dto.Description,
            Order = dto.Order,
            IsActive = dto.IsActive,
            TrackId = trackId
        };
        track.Levels.Add(level);
        await _trackRepository.UpdateAsync(track);
        return level;
    }

    public async Task<TrackStructureDTO> GetLevelsWithStagesAsync(int trackId)
    {
        var track = await _trackRepository.GetTrackWithLevelsAsync(trackId);
        return new TrackStructureDTO
        {
            TrackId = track.Id,
            TrackName = track.Name,
            Levels = track.Levels.Select(l => new LevelStructureDTO
            {
                LevelId = l.Id,
                LevelName = l.Name,
                Stages = l.Stages.Select(s => new StageCriteriaDTO
                {
                    StageId = s.Id,
                    StageName = s.Name,
                    StageType = s.Type.ToString(),
                    EvaluationCriteria = new List<string>()
                }).ToList()
            }).ToList()
        };
    }

    public async Task<TrackStructureDTO> GetLevelsStagesCriteriaAsync(int trackId)
    {
        var track = await _trackRepository.GetTrackWithLevelsAsync(trackId);
        return new TrackStructureDTO
        {
            TrackId = track.Id,
            TrackName = track.Name,
            Levels = track.Levels.Select(l => new LevelStructureDTO
            {
                LevelId = l.Id,
                LevelName = l.Name,
                Stages = l.Stages.Select(s => new StageCriteriaDTO
                {
                    StageId = s.Id,
                    StageName = s.Name,
                    StageType = s.StageType.ToString(),
                    EvaluationCriteria = s.EvaluationCriteria?.Select(ec => ec.Name).ToList() ?? new List<string>()
                }).ToList()
            }).ToList()
        };
    }
}