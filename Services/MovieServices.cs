namespace FilmAPI.Services
{
    public class MovieServices : IMovieServices
    {
        private readonly ApplicaionDbContext _context;

        public MovieServices(ApplicaionDbContext context)
        {
            _context = context;
        }

        public async Task<_Movie> CreateMovie(_Movie movie)
        {
            await _context.Movies.AddAsync(movie);
            await _context.SaveChangesAsync();
            return movie;
        }

        public async Task DeleteMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);

            if (movie == null)
                throw new KeyNotFoundException("Movie not found!");

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<_Movie>> GetAllMovies(byte genreId = 0)
        {
            return await _context.Movies
                .Where(m => genreId == 0 || m.GenreId == genreId)
                .OrderByDescending(m => m.Rate)
                .Include(m => m.Genre)
                .ToListAsync();
        }

        public async Task<_Movie?> GetMovieById(int id)
        {
            return await _context.Movies
        .Include(m => m.Genre)
        .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<_Movie> UpdateMovie(_Movie movie)
        {
            _context.Movies.Update(movie);
            await _context.SaveChangesAsync();
            return movie;
        }

        public async Task<_Movie?> VerifyMovie(string title, int year)
        {
            return await _context.Movies
                .Include(m => m.Genre)
                .FirstOrDefaultAsync(m => m.Title == title && m.year == year);
        }

    }
}