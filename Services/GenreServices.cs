
namespace FilmAPI.Services
{
    public class GenreServices : IGenreServices
    {

        private readonly ApplicaionDbContext _context;

        public GenreServices(ApplicaionDbContext context)
        {
            _context = context;
        }
        public async Task<Genre> CreateGenre(Genre genre)
        {
            await _context.AddAsync(genre);
            await _context.SaveChangesAsync();
            return genre;
        }

        public async Task<Genre> DeleteGenre(byte id)
        {
            var genre = await _context.Genres.FindAsync(id);
            _context.Genres.Remove(genre);
            await _context.SaveChangesAsync();
            return genre;
        }

        public async Task<IEnumerable<Genre>> GetAllGenres()
        {
            return await _context.Genres.ToListAsync();
        }

        public async Task<Genre> GetGenreById(byte id)
        {
            return await _context.Genres.FindAsync(id);
        }


        public async Task<Genre> UpdateGenre(byte id, Genre genre)
        {
            _context.Genres.Update(genre);
            await _context.SaveChangesAsync();
            return genre;
        }
    }
}
