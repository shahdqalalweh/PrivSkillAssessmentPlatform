using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkillAssessmentPlatform.Core.Entities;
using SkillAssessmentPlatform.Core.Interfaces;
using SkillAssessmentPlatform.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;
using SkillAssessmentPlatform.Core.Interfaces.Repository;
using SkillAssessmentPlatform.Infrastructure.Data;
namespace SkillAssessmentPlatform.Application.Services
{
    public class TrackService
    {
        private readonly ITrackRepository _trackRepository;
        private readonly AppDbContext appDbContext;

        public TrackService(ITrackRepository trackRepository, AppDbContext appDbContext)
        {
            _trackRepository = trackRepository;
            this.appDbContext = appDbContext;
        }

        // استرجاع جميع المسارات وتحويلها إلى DTO
        public async Task<IEnumerable<TrackDto>> GetAllTracksAsync()
        {
            var tracks = await _trackRepository.GetAllAsync();
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

        // استرجاع مسار محدد بالمعرف
        public async Task<TrackDto> GetTrackByIdAsync(int id)
        {
            var track = await _trackRepository.GetByIdAsync(id);
            if (track == null) return null;
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

        // إنشاء مسار جديد
        public async Task<TrackDto> CreateTrackAsync(TrackDto trackDto)
        {
            var track = new Track
            {
                // Id يتم توليده من قاعدة البيانات
              //  Id = trackDto.Id,
                SeniorExaminerID = trackDto.SeniorExaminerID,
                Name = trackDto.Name,
                Description = trackDto.Description,
                Objectives = trackDto.Objectives,
                AssociatedSkills = trackDto.AssociatedSkills,
                IsActive = trackDto.IsActive,
                
                Image = trackDto.Image,
                CreatedAt = System.DateTime.UtcNow
            };
            ///>> assign role to senior examiner via examiner repo 
            await _trackRepository.AddAsync(track);
            trackDto.Id = track.Id;
            return trackDto;
        }

        // تحديث بيانات مسار موجود
        public async Task<bool> UpdateTrackAsync(int id, TrackDto trackDto)
        {
            var track = await _trackRepository.GetByIdAsync(id);
            if (track == null) return false;
            track.SeniorExaminerID = trackDto.SeniorExaminerID;
            track.Name = trackDto.Name;
            track.Description = trackDto.Description;
            track.Objectives = trackDto.Objectives;
            track.AssociatedSkills = trackDto.AssociatedSkills;
            track.IsActive = trackDto.IsActive;
            track.Image = trackDto.Image;
            await _trackRepository.UpdateAsync(track);
            return true;
        }

        // حذف مسار
        public async Task<bool> DeleteTrackAsync(int id)
        {
            var track = await _trackRepository.GetByIdAsync(id);
            if (track == null) return false;
            await _trackRepository.DeleteAsync(id);
            return true;
        }

        // استرجاع المستويات الخاصة بمسار معين
        public async Task<IEnumerable<LevelDto>> GetLevelsByTrackIdAsync(int trackId)
        {
            var levels = await _trackRepository.GetLevelsByTrackIdAsync(trackId);
           
            var data =  levels.Select(l => new LevelDto
            {
                Id = l.Id,
                StageName = appDbContext.Stages.FirstOrDefault(x => x.LevelId == l.Id)?.Name??"Not found",
                TrackId = l.TrackId,
                Name = l.Name,
                Description = l.Description,
                Order = l.Order,
                IsActive = l.IsActive
            }).ToList();

            return data;

        }

        // إنشاء مستوى جديد داخل مسار
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
            await _trackRepository.AddLevelAsync(trackId, level);
            levelDto.Id = level.Id;
            levelDto.TrackId = trackId;
            return levelDto;
        }

        // إسناد ممتحن لمسار
        public async Task<bool> AssignExaminerAsync(int trackId, ExaminerAssignmentDto assignmentDto)
        {
            await _trackRepository.AssignExaminerAsync(trackId, assignmentDto.ExaminerId);
            return true;
        }

        // إزالة ممتحن من مسار
        public async Task<bool> RemoveExaminerAsync(int trackId, string examinerId)
        {
            await _trackRepository.RemoveExaminerAsync(trackId, examinerId);
            return true;
        }
    }
}
