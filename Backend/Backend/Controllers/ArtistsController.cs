using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Models;
using Microsoft.AspNetCore.Authorization;

namespace Backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ArtistsController : ControllerBase
    {
        private readonly NorvoDBContext _context;

        public ArtistsController(NorvoDBContext context)
        {
            _context = context;
        }

        // GET: api/Artists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Artist>>> GetArtists()
        {
            return await _context.Artists.ToListAsync();
        }

        // GET: api/Artists/5
        [HttpGet("{name}")]
        public async Task<ActionResult<IEnumerable<Artist>>> GetArtist(string name)
        {
            return await _context.Artists.Where(prop=>prop.ArtistName.StartsWith(name)).ToListAsync();
        }

        [HttpGet("id/{id}")]
        public async Task<ActionResult<Artist>> GetArtistById(int id)
        {
            return await _context.Artists.Where(prop => prop.ArtistId==id).FirstOrDefaultAsync();
        }

        [HttpGet("allwithsongs")]
        public async Task<ActionResult<IEnumerable<Artist>>> GetArtistsWithSongs()
        {
            return await _context.Artists.Include(prop=>prop.Songs).ToListAsync();
        }
        [HttpGet("{name}/withsongs")]
        public async Task<ActionResult<IEnumerable<Artist>>> GetArtistWithSongs(string name)
        {
            return await _context.Artists.Where(prop=>prop.ArtistName==name).Include(prop => prop.Songs).ToListAsync();
        }

        [HttpGet("allartistsfull")]
        public async Task<ActionResult<IEnumerable<Artist>>> GetArtistsFull()
        {
            return await _context.Artists.Include(prop => prop.Songs).ThenInclude(opt => opt.Lyrics).ToListAsync();
        }

        [HttpGet("allartistfull/{name}")]
        public async Task<ActionResult<IEnumerable<Artist>>> GetArtistFull(string name)
        {
            return await _context.Artists.Where(prop => prop.ArtistName == name).Include(prop => prop.Songs).ThenInclude(opt => opt.Lyrics).ToListAsync();
        }


        [HttpGet("search/{name}")]
        public async Task<ActionResult<IEnumerable<Artist>>> SearchName(string name)
        {
            return await _context.Artists.Where(prop => prop.ArtistName.Contains(name)).ToListAsync();
        }


        // PUT: api/Artists/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArtist(int id, Artist artist)
        {
            if (id != artist.ArtistId)
            {
                return BadRequest();
            }

            _context.Entry(artist).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArtistExists(id))
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

        // POST: api/Artists
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Artist>> PostArtist(Artist artist)
        {
            _context.Artists.Add(artist);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetArtist", new { id = artist.ArtistId }, artist);
        }

        // DELETE: api/Artists/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Artist>> DeleteArtist(int id)
        {
            var artist = await _context.Artists.FindAsync(id);
            if (artist == null)
            {
                return NotFound();
            }

            _context.Artists.Remove(artist);
            await _context.SaveChangesAsync();

            return artist;
        }

        private bool ArtistExists(int id)
        {
            return _context.Artists.Any(e => e.ArtistId == id);
        }
    }
}
