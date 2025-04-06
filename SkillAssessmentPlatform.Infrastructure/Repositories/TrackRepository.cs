using Microsoft.EntityFrameworkCore;
using SkillAssessmentPlatform.Core.Entities;
using SkillAssessmentPlatform.Core.Interfaces;
using SkillAssessmentPlatform.Core.Interfaces.Repository;
using SkillAssessmentPlatform.Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkillAssessmentPlatform.Infrastructure.Repositories
{
    public class TrackRepository : ITrackRepository
    {
        private readonly AppDbContext _context;

        public TrackRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Track>> GetAllAsync()
        {
            return await _context.Tracks
                .Include(t => t.Levels)
                .ToListAsync();
        }

        public async Task<Track> GetByIdAsync(int id)
        {
            return await _context.Tracks
                .Include(t => t.Levels)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task AddAsync(Track track)
        {
            await _context.Tracks.AddAsync(track);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Track track)
        {
            _context.Tracks.Update(track);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var track = await GetByIdAsync(id);
            if (track != null)
            {
                _context.Tracks.Remove(track);
                await _context.SaveChangesAsync();
            }
        }

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

        public async Task AssignExaminerAsync(int trackId, string examinerId)
        {
            // يتم الحصول على Track مع مجموعة Examiners
            var track = await _context.Tracks
                .Include(t => t.Examiners)
                .FirstOrDefaultAsync(t => t.Id == trackId);
            if (track != null)
            {
                // هنا عليك تنفيذ منطق إضافة الممتحن إلى مجموعة Examiners
                // مثال: إذا كانت العلاقة عبر جدول وسيط أو مباشرة، قم بتعديل الكود وفقًا لذلك.
                // track.Examiners.Add(new Examiner { Id = examinerId });
                await _context.SaveChangesAsync();
            }
        }

        public async Task RemoveExaminerAsync(int trackId, string examinerId)
        {
            var track = await _context.Tracks
                .Include(t => t.Examiners)
                .FirstOrDefaultAsync(t => t.Id == trackId);
            if (track != null)
            {
                // مثال: العثور على الممتحن وإزالته من المجموعة
                // var examiner = track.Examiners.FirstOrDefault(e => e.Id == examinerId);
                // if (examiner != null)
                // {
                //     track.Examiners.Remove(examiner);
                // }
                await _context.SaveChangesAsync();
            }
        }
    }
}
