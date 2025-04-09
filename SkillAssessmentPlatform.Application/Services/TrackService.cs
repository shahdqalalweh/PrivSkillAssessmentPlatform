
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SkillAssessmentPlatform.Application.DTOs;
using SkillAssessmentPlatform.Core.Entities;
using SkillAssessmentPlatform.Core.Entities.Feedback_and_Evaluation;
using SkillAssessmentPlatform.Core.Interfaces;
using SkillAssessmentPlatform.Infrastructure.Data;

namespace SkillAssessmentPlatform.Application.Services
{
    public class TrackService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<TrackService> _logger;

        public TrackService(
            IUnitOfWork unitOfWork,
            AppDbContext appDbContext,
            ILogger<TrackService> logger)
        {
            _unitOfWork = unitOfWork;
            _appDbContext = appDbContext;
            _logger = logger;
        }

        public async Task<IEnumerable<TrackDto>> GetAllTracksAsync()
        {
            var tracks = await _unitOfWork.TrackRepository.GetAllAsync();
            return tracks.Select(t => new TrackDto
            {
                Id = t.Id,
                SeniorExaminerID = t.SeniorExaminerID,
                Name = t.Name,
                Description = t.Description,
                Objectives = t.Objectives,
                AssociatedSkills = t.AssociatedSkills,
                IsActive = t.IsActive,
                Image = t.Image
            });
        }

        public async Task<TrackDto> GetTrackByIdAsync(int id)
        {
            var track = await _unitOfWork.TrackRepository.GetByIdAsync(id);
           // if (track == null) return null;

            return new TrackDto
            {
                Id = track.Id,
                SeniorExaminerID = track.SeniorExaminerID,
                Name = track.Name,
                Description = track.Description,
                Objectives = track.Objectives,
                AssociatedSkills = track.AssociatedSkills,
                IsActive = track.IsActive,
                Image = track.Image
            };
        }

        public async Task<TrackDto> CreateTrackAsync(TrackDto trackDto)
        {
            var track = new Track
            {
                SeniorExaminerID = trackDto.SeniorExaminerID,
                Name = trackDto.Name,
                Description = trackDto.Description,
                Objectives = trackDto.Objectives,
                AssociatedSkills = trackDto.AssociatedSkills,
                IsActive = trackDto.IsActive,
                Image = trackDto.Image,
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.TrackRepository.AddAsync(track);
            await _unitOfWork.SaveChangesAsync();

            trackDto.Id = track.Id;
            return trackDto;
        }

        public async Task<bool> UpdateTrackAsync(int id, TrackDto trackDto)
        {
            var track = await _unitOfWork.TrackRepository.GetByIdAsync(id);
            if (track == null) return false;

            track.SeniorExaminerID = trackDto.SeniorExaminerID;
            track.Name = trackDto.Name;
            track.Description = trackDto.Description;
            track.Objectives = trackDto.Objectives;
            track.AssociatedSkills = trackDto.AssociatedSkills;
            track.IsActive = trackDto.IsActive;
            track.Image = trackDto.Image;

            await _unitOfWork.TrackRepository.UpdateAsync(track);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteTrackAsync(int id)
        {
            var track = await _unitOfWork.TrackRepository.GetByIdAsync(id);
            if (track == null) return false;

            await _unitOfWork.TrackRepository.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<LevelDto>> GetLevelsByTrackIdAsync(int trackId)
        {
            var levels = await _unitOfWork.TrackRepository.GetLevelsByTrackIdAsync(trackId);

            var data = levels.Select(l => new LevelDto
            {
                Id = l.Id,
                StageName = _appDbContext.Stages.FirstOrDefault(x => x.LevelId == l.Id)?.Name ?? "Not found",
                TrackId = l.TrackId,
                Name = l.Name,
                Description = l.Description,
                Order = l.Order,
                IsActive = l.IsActive
            }).ToList();

            return data;
        }

        public async Task<Level> CreateLevelAsync(int trackId, CreateLevelDTO dto)
        {
            var track = await _unitOfWork.TrackRepository.GetTrackWithLevelsAsync(trackId);
            var level = new Level
            {
                Name = dto.Name,
                Description = dto.Description,
                Order = dto.Order,
                IsActive = dto.IsActive,
                TrackId = trackId
            };
            track.Levels.Add(level);
            await _unitOfWork.TrackRepository.UpdateAsync(track);
            return level;
        }

        public async Task<LevelDto> CreateLevelAsync(int trackId, LevelDto levelDto)
        {
            var level = new Level
            {
                TrackId = trackId,
                Name = levelDto.Name,
                Description = levelDto.Description,
                Order = levelDto.Order,
                IsActive = levelDto.IsActive
            };

            await _unitOfWork.TrackRepository.AddLevelAsync(trackId, level);
            await _unitOfWork.SaveChangesAsync();

            levelDto.Id = level.Id;
            levelDto.TrackId = trackId;
            return levelDto;
        }

        public async Task<bool> AssignExaminerAsync(int trackId, ExaminerAssignmentDto assignmentDto)
        {
            await _unitOfWork.TrackRepository.AssignExaminerAsync(trackId, assignmentDto.ExaminerId);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveExaminerAsync(int trackId, string examinerId)
        {
            await _unitOfWork.TrackRepository.RemoveExaminerAsync(trackId, examinerId);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CreateTrackStructureAsync(TrackStructureDTO structureDTO)
        {
            var track = await _unitOfWork.TrackRepository.GetByIdAsync(structureDTO.TrackId);
            if (track == null)
                throw new KeyNotFoundException($"Track with ID {structureDTO.TrackId} not found");

            using (var transaction = await _unitOfWork.BeginTransactionAsync())
            {
                try
                {
                    foreach (var levelDTO in structureDTO.Levels)
                    {
                        var level = new Level
                        {
                            TrackId = structureDTO.TrackId,
                            Name = levelDTO.Name,
                            Description = levelDTO.Description,
                            Order = levelDTO.Order,
                            IsActive = true
                        };

                        await _unitOfWork.LevelRepository.AddAsync(level);
                        await _unitOfWork.SaveChangesAsync();

                        foreach (var stageDTO in levelDTO.Stages)
                        {
                            var stage = new Stage
                            {
                                LevelId = level.Id,
                                Name = stageDTO.Name,
                                Description = stageDTO.Description,
                                Type = stageDTO.Type,
                                Order = stageDTO.Order,
                                IsActive = true,
                                PassingScore = stageDTO.PassingScore
                            };

                            await _unitOfWork.StageRepository.AddAsync(stage);
                            await _unitOfWork.SaveChangesAsync();

                            foreach (var criteriaDTO in stageDTO.EvaluationCriteria)
                            {
                                var criteria = new EvaluationCriteria
                                {
                                    StageId = stage.Id,
                                    Name = criteriaDTO.Name,
                                    Description = criteriaDTO.Description,
                                    Weight = criteriaDTO.Weight
                                };

                                await _unitOfWork.EvaluationCriteriaRepository.AddAsync(criteria);
                            }

                            await _unitOfWork.SaveChangesAsync();
                        }
                    }

                    await transaction.CommitAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    _logger.LogError(ex, "Error creating track structure for track ID {TrackId}", structureDTO.TrackId);
                    throw new Exception("Failed to create track structure", ex);
                }
            }
        }

        public async Task<TrackStructureDTO> GetLevelsWithStagesAsync(int trackId)
        {
            var track = await _unitOfWork.TrackRepository.GetTrackWithLevelsAsync(trackId);
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
            var track = await _unitOfWork.TrackRepository.GetTrackWithLevelsAsync(trackId);
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
                        EvaluationCriteria = s.EvaluationCriteria?.Select(ec => ec.Name).ToList() ?? new List<string>()
                    }).ToList()
                }).ToList()
            };
        }
    }
}
