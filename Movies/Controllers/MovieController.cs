using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movies.Data;
using Movies.Models;
using Movies.Extensions;

namespace Movies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly MovieContext _context;

        public MoviesController(MovieContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieModel>>> GetMovies()
        {
            return await _context.Movies.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MovieDTO>> GetMovie(Guid id)
        {
            var movie = await _context.Movies.FindAsync(id);

            if (movie == null)
            {
                return NotFound();
            }

            return movie.ToDTO();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(Guid id, MovieDTO movieDTO)
        {
            if (id != movieDTO.Id)
            {
                return BadRequest();
            }

            var movieModel = movieDTO.ToModel();
            _context.Entry(movieModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(id))
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

        [HttpPost]
        public async Task<ActionResult<MovieDTO>> PostMovie(MovieDTO movieDTO)
        {
            var movieModel = movieDTO.ToModel();
            _context.Movies.Add(movieModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMovie", new { id = movieModel.Id }, movieDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(Guid id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MovieExists(Guid id)
        {
            return _context.Movies.Any(e => e.Id == id);
        }
    }
}
