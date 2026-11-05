namespace FilmAPI.Services
{
    public interface IGenreServices
    {
        Task<IEnumerable<Genre>> GetAllGenres();
        Task<Genre> CreateGenre(Genre genre);
        Task<Genre> UpdateGenre(byte id, Genre genre);
        Task<Genre> DeleteGenre(byte id);
        Task<Genre> GetGenreById(byte id);

    }
}
