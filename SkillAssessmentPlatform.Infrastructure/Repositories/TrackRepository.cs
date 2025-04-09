﻿using Microsoft.EntityFrameworkCore;
using SkillAssessmentPlatform.Core.Entities;
using SkillAssessmentPlatform.Core.Entities.Users;
using SkillAssessmentPlatform.Core.Interfaces;
using SkillAssessmentPlatform.Core.Interfaces.Repository;
using SkillAssessmentPlatform.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SkillAssessmentPlatform.Core.Entities;
using SkillAssessmentPlatform.Core.Entities.Users;
using SkillAssessmentPlatform.Core.Enums;
using SkillAssessmentPlatform.Core.Exceptions;
using SkillAssessmentPlatform.Core.Interfaces.Repository;
using SkillAssessmentPlatform.Infrastructure.Data;

namespace SkillAssessmentPlatform.Infrastructure.Repositories
{
    public class TrackRepository : GenericRepository<Track> , ITrackRepository

    {
        //private readonly AppDbContext _context;

        public async Task<Track> GetTrackWithDetailsAsync(int trackId)
        {
            return await _context.Tracks
                .Include(t => t.Levels)
                    .ThenInclude(l => l.Stages)
                        .ThenInclude(s => s.EvaluationCriteria)
                .FirstOrDefaultAsync(t => t.Id == trackId);
        }

        public TrackRepository(AppDbContext context) : base(context)
        {
        }

        #region IGenericRepository<Track> Members

      /*  public async Task<Track> GetByIdAsync(int id)
        {
            return await _context.Tracks
                .Include(t => t.Levels)
                .FirstOrDefaultAsync(t => t.Id == id);
        }
      */
        // لاستخدام GetByIdAsync(string id) نرفع استثناء أو نتركه غير مدعوم لأن المفتاح int
      /*  public Task<Track> GetByIdAsync(string id)
        {
            throw new NotSupportedException("Track primary key is of type int.");
        }
      */
      /*
        public async Task<IEnumerable<Track>> GetAllAsync()
        {
            return await _context.Tracks
                .Include(t => t.Levels)
                .ToListAsync();
        }
        /*
        public async Task<int> GetCount()
        {
            return await _context.Tracks.CountAsync();
        }*//*
        // important
        public async Task<IEnumerable<Track>> GetPagedAsync(int page, int pageSize)
        {
            return await _context.Tracks
                .OrderBy(t => t.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Track> UpdateAsync(Track entity)
        {
            _context.Tracks.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var track = await GetByIdAsync(id);
            if (track == null)
                return false;
            _context.Tracks.Remove(track);
            await _context.SaveChangesAsync();
            return true;
        }

        public Task<bool> DeleteAsync(string id)
        {
            throw new NotSupportedException("Track primary key is of type int.");
        }

        public async Task<int> GetTotalCountAsync()
        {
            return await _context.Tracks.CountAsync();
        }
        */
        #endregion

        #region ITrackRepository Members

        public async Task<IEnumerable<Level>> GetLevelsByTrackIdAsync(int trackId)
        {
            return await _context.Levels
                .Where(l => l.TrackId == trackId)
                .ToListAsync();
        }

        public async Task AddLevelAsync(int trackId, Level level)
        {
            level.TrackId = trackId;
            await _context.Levels.AddAsync(level);
            await _context.SaveChangesAsync();
        }

        /*
        // Update from Examiner to seniorExaminer
        public async Task AssignSeniorExaminerAsync(int trackId, string examinerId)
        {
            // نفترض أن العلاقة بين Track والـ Examiner مُدارة عبر الخاصية Examiners في Track.
            var track = await _context.Tracks
                .Include(t => t.Examiners)
                .FirstOrDefaultAsync(t => t.Id == trackId);
            if (track != null)
            {
                // إذا كانت العلاقة مباشرةً، فقم بالتحقق إذا كان الممتحن موجودًا بالفعل في القائمة
                // مثال توضيحي: (يجب تعديل هذا الجزء حسب تصميم العلاقة الفعلي)
                if (!track.Examiners.Any(e => e.Id == examinerId))
                {
                    // نفترض إنشاء كائن Examiner مؤقت بالمعرف فقط؛
                    // يُفضل جلب الكائن الكامل من مصدر آخر إذا كان ذلك مطلوبًا.
                    var examiner = new Examiner { Id = examinerId };
                    track.Examiners.Add(examiner);
                    await _context.SaveChangesAsync();
                }
            }
        }
        */
        /*
        //موجودة في اند بوينت ثانية
        public async Task RemoveExaminerAsync(int trackId, string examinerId)
        {
            var track = await _context.Tracks
                .Include(t => t.Examiners)
                .FirstOrDefaultAsync(t => t.Id == trackId);
            if (track != null)
            {
                // البحث عن الممتحن في القائمة وإزالته
                var examiner = track.Examiners.FirstOrDefault(e => e.Id == examinerId);
                if (examiner != null)
                {
                    track.Examiners.Remove(examiner);
                    await _context.SaveChangesAsync();
                }
            }
        }
        */

        public async Task AddAsync(Track track)
        {
           await _context.Tracks.AddAsync(track);
            _context.SaveChanges();
        }

        Task ITrackRepository.UpdateAsync(Track track)
        {
            return UpdateAsync(track);
        }

        Task ITrackRepository.DeleteAsync(int id)
        {
            return DeleteAsync(id);
        }

        public Task AssignExaminerAsync(int trackId, string examinerId)
        {
            throw new NotImplementedException();
        }

        public Task RemoveExaminerAsync(int trackId, string examinerId)
        {
            throw new NotImplementedException();
        }

        public Task<Track> GetTrackWithLevelsAsync(int trackId)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
