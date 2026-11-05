namespace FilmAPI.Dtos
{
    public class MovieDetailsDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int year { get; set; }
        public byte GenreId { get; set; }
        public double Rate { get; set; }
        public string Description { get; set; }
        public byte[] Poster { get; set; }
        public String GenreName { get; set; }
    }
}
