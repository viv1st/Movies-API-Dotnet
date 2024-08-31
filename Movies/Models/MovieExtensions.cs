using Movies.Models;
using System.Globalization;

namespace Movies.Extensions
{
    public static class MovieExtensions
    {
        public static MovieDTO ToDTO(this MovieModel movieModel)
        {
            if (movieModel == null)
            {
                throw new ArgumentNullException(nameof(movieModel));
            }

            return new MovieDTO
            {
                Title = movieModel.Title,
                Summary = movieModel.Summary,
                Poster = movieModel.Poster,
                ReleaseDateYMD = movieModel.ReleaseDateYMD,
            };
        }

        public static MovieModel ToModel(this MovieDTO movieDTO)
        {
            if (movieDTO == null)
            {
                throw new ArgumentNullException(nameof(movieDTO));
            }

            var releaseDate = movieDTO.ReleaseDateYMD.HasValue
                ? movieDTO.ReleaseDateYMD.Value.ToString("d MMMM yyyy", new CultureInfo("fr-FR"))
                : "N/A";

            return new MovieModel
            {
                Id = Guid.NewGuid(),
                Title = movieDTO.Title,
                Summary = movieDTO.Summary,
                Poster = movieDTO.Poster,
                Popularity = 0,
                AverageRating = 0,
                VotingPeople = 0,
                ReleaseDateYMD = movieDTO.ReleaseDateYMD,
                ReleaseDate = releaseDate,
                ItemCreated = DateTimeOffset.Now.ToUnixTimeMilliseconds()
            };
        }
    }
}
