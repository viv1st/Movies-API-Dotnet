namespace Movies.Models
{
    public class MovieDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Poster { get; set; }
        public DateTime? ReleaseDateYMD { get; set; }
    }
}
