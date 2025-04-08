
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkillAssessmentPlatform.Core.Entities;
using SkillAssessmentPlatform.Infrastructure.Data;

namespace SkillAssessmentPlatform.API.Controllers
    {
        //  [Route("api/[controller]")]
        [Route("/track")]

        [ApiController]
        public class TracksController : ControllerBase
        {
            private readonly AppDbContext _context;

            public TracksController(AppDbContext context)
            {
                _context = context;
            }

            // GET: api/TrackApi
            [HttpGet]
            public async Task<ActionResult<IEnumerable<Track>>> GetTracks()
            {
                return await _context.Tracks.ToListAsync();
            }

            // GET: api/TrackApi/5
            [HttpGet("{id}")]
            public async Task<ActionResult<Track>> GetTrack(int id)
            {
                var track = await _context.Tracks.FindAsync(id);

                if (track == null)
                {
                    return NotFound();
                }

                return track;
            }

            // PUT: api/TrackApi/5
            // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
            [HttpPut("{id}")]
            public async Task<IActionResult> PutTrack(int id, Track track)
            {
                if (id != track.Id)
                {
                    return BadRequest();
                }

                _context.Entry(track).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrackExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return NoContent();
            }

            // POST: api/TrackApi
            // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
            [HttpPost]
            public async Task<ActionResult<Track>> PostTrack(Track track)
            {
                _context.Tracks.Add(track);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetTrack", new { id = track.Id }, track);
            }

            // DELETE: api/TrackApi/5
            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteTrack(int id)
            {
                var track = await _context.Tracks.FindAsync(id);
                if (track == null)
                {
                    return NotFound();
                }

                _context.Tracks.Remove(track);
                await _context.SaveChangesAsync();

                return NoContent();
            }

            private bool TrackExists(int id)
            {
                return _context.Tracks.Any(e => e.Id == id);
            }






            // GET: api/TrackApi/5
            [HttpGet("{id}/levels")]
            public async Task<ActionResult<Track>> TrackLevel(int id)
            {
                var track = await _context.Tracks.Include(a => a.Levels).FirstOrDefaultAsync(a => a.Id == id);


                if (track == null)
                {
                    return NotFound();
                }

                return Ok(track.Levels);
            }
        }
    }


