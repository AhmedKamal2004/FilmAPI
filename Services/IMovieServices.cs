namespace FilmAPI.Services
{
    public interface IMovieServices
    {
        Task<IEnumerable<_Movie>> GetAllMovies(byte genreId=0);
        Task<_Movie?> VerifyMovie(string title,int year);
        Task<_Movie?> GetMovieById(int id);
        Task<_Movie>  CreateMovie(_Movie movie);
        //Task<_Movie> UpdateMovie(int id, _Movie movie);
        Task DeleteMovie(int id);
        Task<_Movie> UpdateMovie(_Movie movie);
        

    }
}
