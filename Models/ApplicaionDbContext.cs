using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace FilmAPI.Models
{

    public class ApplicaionDbContext : DbContext
    {

        private readonly string _connectionString;
        public ApplicaionDbContext(DbContextOptions<ApplicaionDbContext> options) : base(options)
        {
        }

        public ApplicaionDbContext(string Cs)
        {
            _connectionString = Cs;
        }

        public DbSet<Genre> Genres { get; set; }
        public DbSet<_Movie> Movies { get; set; }

       
    }
}
