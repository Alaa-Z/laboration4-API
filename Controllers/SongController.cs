using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestAPI.Data;
using TestAPI.Models;

namespace TestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongController : ControllerBase
    {
        private readonly MyContext _context;

        public SongController(MyContext context)
        {
            _context = context;
        }

        // GET all songs 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Song>>> GetSongs()
        {
            if (_context.Songs == null)
        {
            return NotFound();
        }

        // Include the Artist, Category in the respons and then chose id, title, length, ArtistName,CategoryName
        var songs = await _context.Songs
        .Include(s => s.Artist)
        .Include(s => s.Category)
        .Select(s => new
        {
            Id = s.Id,
            Title = s.Title,
            Length = s.Length,
            ArtistName = s.Artist.Name,
            CategoryName = s.Category.Name
        })

        .ToListAsync();
        return Ok(songs);
        }


        // GET: api/Song/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Object>> GetSong(int id)
        {
            if (_context.Songs == null)
            {
                return NotFound();
            }

            var song = await _context.Songs
            .Include(s => s.Artist)
            .Include(s => s.Category)
            .Where(s => s.Id == id)
            .Select(s => new
                {
                    Id = s.Id,
                    Title = s.Title,
                    Length = s.Length,
                    ArtistName = s.Artist.Name,
                    CategoryName = s.Category.Name
                })
            .FirstOrDefaultAsync(); // return the first matching from db 

            if (song == null)
            {
                return NotFound();
            }

            return song;
        }


        // PUT: api/Song/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSong(int id, Song song)
        {
            if (id != song.Id)
            {
                return BadRequest();
            }

            // Controll if the Artist already exists in db
            var artist = await _context.Artists.SingleOrDefaultAsync(a => a.Name == song.Artist.Name);
            if (artist == null)
            {
                //create a new artist
                artist = new Artist { Name = song.Artist?.Name };
                _context.Artists.Add(artist);
            }

            // Controll if the category already exists in db
            var category = await _context.Categories.SingleOrDefaultAsync(c => c.Name == song.Category.Name);
            if (category == null)
            {
                //create a new category
                category = new Category { Name = song.Category?.Name };
                _context.Categories.Add(category);
            }

            // Create the new Song object
            var newSong = new Song
            {
                Id = song.Id,
                Title = song.Title,
                Length = song.Length,
                Artist = artist,
                Category = category
            };

            _context.Entry(newSong).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SongExists(id))
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



        // POST: api/Song
        [HttpPost]
        public async Task<ActionResult<Song>> PostSong(Song song)
        {
            // Controll if the Artist already exists in db
            var artist = await _context.Artists.SingleOrDefaultAsync(a => a.Name == song.Artist.Name);
            if (artist == null)
            {
                //create a new artist
                artist = new Artist { Name = song.Artist?.Name };
                _context.Artists.Add(artist);
            }

            // Controll if the category already exists in db
            var category = await _context.Categories.SingleOrDefaultAsync(c => c.Name == song.Category.Name);
            if (category == null)
            {
                // create a new category
                category = new Category { Name = song.Category?.Name };
                _context.Categories.Add(category);
            }

            // Create new Song  
            var newSong = new Song
            {
                Title = song.Title,
                Length = song.Length,
                Artist = artist,
                Category = category
            };

            // Add new Song to db
            _context.Songs.Add(newSong);
            await _context.SaveChangesAsync();

            // Return the new Song object
            return CreatedAtAction(nameof(GetSong), new { id = newSong.Id }, newSong);
        }


        // DELETE: api/Song/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSong(int id)
        {
            if (_context.Songs == null)
            {
                return NotFound();
            }
            var song = await _context.Songs.FindAsync(id);
            if (song == null)
            {
                return NotFound();
            }

            _context.Songs.Remove(song);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SongExists(int id)
        {
            return (_context.Songs?.Any(e => e.Id == id)).GetValueOrDefault();
        }


        // GET list of songs for artist 
        [HttpGet("artist/{artistId}/songs")]
        public async Task<ActionResult<IEnumerable<Song>>> GetSongsByArtist(int artistId)
        {
            // Find the artist by the ID
            var artist = await _context.Artists.FindAsync(artistId);

            if (artist == null)
            {
                return NotFound();
            }

            // Get songs by the artist id
            var songs = await _context.Songs.Where(s => s.ArtistID == artistId)
               .Select(s => new
               {
                   Id = s.Id,
                   Title = s.Title
               })

               .ToListAsync();
            return Ok(songs);

        }

        // GET list of songs for category 
        [HttpGet("category/{categoryId}/songs")]
        public async Task<ActionResult<IEnumerable<Song>>> GetSongsByCategory(int categoryId)
        {
            // Find the category by ID
            var category = await _context.Categories.FindAsync(categoryId);

            if (category == null)
            {
                return NotFound();
            }

            // Get all songs by the category id
            var songs = await _context.Songs.Where(s => s.CategoryID == categoryId)
               .Select(s => new
               {
                   Id = s.Id,
                   Title = s.Title
               })

               .ToListAsync();
            return Ok(songs);

        }
    }

}
