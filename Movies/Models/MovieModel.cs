namespace Movies.Models
{
    public class MovieModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Poster { get; set; }
        public float Popularity { get; set; } 
        public float AverageRating { get; set; }
        public int VotingPeople { get; set; }
        public DateTime? ReleaseDateYMD { get; set; }
        public string ReleaseDate { get; set; }
        public long ItemCreated { get; set; }
    }
}
