using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movies.Data;
using Movies.Models;
using Movies.Extensions;
using System.Globalization;
using Microsoft.AspNetCore.Authorization;

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
        [Authorize]
        public async Task<ActionResult<IEnumerable<MovieModel>>> GetMovies()
        {
            return await _context.movies.ToListAsync();
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<MovieModel>> GetMovie(Guid id)
        {
            var movie = await _context.movies.FindAsync(id);

            if (movie == null)
            {
                return NotFound();
            }

            return movie;
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutMovie(Guid id, MovieDTO movieDTO)
        {
            var movie = await _context.movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            movie.Title = movieDTO.Title;
            movie.Summary = movieDTO.Summary;
            movie.Poster = movieDTO.Poster;
            movie.ReleaseDateYMD = movieDTO.ReleaseDateYMD;
            movie.ReleaseDate = movieDTO.ReleaseDateYMD.HasValue
                ? movieDTO.ReleaseDateYMD.Value.ToString("d MMMM yyyy", new CultureInfo("fr-FR"))
                : "N/A";

            _context.Entry(movie).State = EntityState.Modified;

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
        [Authorize]
        public async Task<ActionResult<MovieDTO>> PostMovie(MovieDTO movieDTO)
        {
            var movieModel = movieDTO.ToModel();
            _context.movies.Add(movieModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMovie", new { id = movieModel.Id }, movieDTO);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteMovie(Guid id)
        {
            var movie = await _context.movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            _context.movies.Remove(movie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MovieExists(Guid id)
        {
            return _context.movies.Any(e => e.Id == id);
        }
    }
}
