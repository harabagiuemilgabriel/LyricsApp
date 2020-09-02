using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Models;

namespace Backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LyricsController : ControllerBase
    {
        private readonly NorvoDBContext _context;

        public LyricsController(NorvoDBContext context)
        {
            _context = context;
        }

        // GET: api/Lyrics
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Lyric>>> GetLyrics()
        {
            return await _context.Lyrics.ToListAsync();
        }

        // GET: api/Lyrics/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Lyric>> GetLyric(int id)
        {
            return await _context.Lyrics.Where(prop=>prop.Song==id).FirstOrDefaultAsync();
        }

        [HttpGet("search/{sentence}")]
        public async Task<ActionResult<List<Lyric>>> GetLyricThatContains(string sentence)
        {
            return await _context.Lyrics.Where(prop=>prop.Lyrics.Contains(sentence)).ToListAsync();
        }


        // PUT: api/Lyrics/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLyric(int id, Lyric lyric)
        {
            if (id != lyric.LyricsId)
            {
                return BadRequest();
            }

            _context.Entry(lyric).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LyricExists(id))
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

        // POST: api/Lyrics
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Lyric>> PostLyric(Lyric lyric)
        {
            _context.Lyrics.Add(lyric);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLyric", new { id = lyric.LyricsId }, lyric);
        }

        // DELETE: api/Lyrics/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Lyric>> DeleteLyric(int id)
        {
            var lyric = await _context.Lyrics.FindAsync(id);
            if (lyric == null)
            {
                return NotFound();
            }

            _context.Lyrics.Remove(lyric);
            await _context.SaveChangesAsync();

            return lyric;
        }

        private bool LyricExists(int id)
        {
            return _context.Lyrics.Any(e => e.LyricsId == id);
        }
    }
}
