using Microsoft.EntityFrameworkCore;
using Movies.Models;

namespace Movies.Data
{
    public class MovieContext : DbContext
    {
        public MovieContext(DbContextOptions<MovieContext> options) : base(options)
        {
        }

        public DbSet<MovieModel> Movies { get; set; }
    }
}
