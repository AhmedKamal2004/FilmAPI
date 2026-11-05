namespace FilmAPI.Dtos
{
    public class MoviesDto
    {
        [MaxLength(length: 250)]
        public string Title { get; set; }
        public int year { get; set; }
        public byte GenreId { get; set; }
        public double Rate { get; set; }

        [MaxLength(length: 2500)]
        public string Description { get; set; }
        public IFormFile? Poster { get; set; }
    }
}
