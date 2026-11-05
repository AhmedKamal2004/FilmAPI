namespace FilmAPI.Dtos
{
    public class CreateGenreDto
    {

        [MaxLength(length: 100)]
        public string Name { get; set; }
    }
}
