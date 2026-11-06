using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
namespace FilmAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMovieServices _movieServices;
        private readonly IGenreServices _genreServices;
        private new List<string> _AllowedExtention = new List<string> { ".jpg", ".png" };
        private long _MaxAllowedPosterSize = 1048576;
        public MoviesController(IMovieServices MovieServices, IGenreServices GenreServices ,IMapper mapper)
        {
          
            _movieServices = MovieServices  ;
            _genreServices = GenreServices;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] MoviesDto dto)
        {
            // Logic to create a new movie
            var exists = _movieServices.VerifyMovie(dto.Title, dto.year).Result != null;


            if (dto.Poster == null)
                return BadRequest("Poster is required!");
            
            var IsValidGenre = await _genreServices.GetGenreById(dto.GenreId)!= null;
            if (!IsValidGenre)
                return BadRequest("Invalid Genre Id!");

            if (exists)
                return Conflict("This movie already exists!");

            if (!_AllowedExtention.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
                return BadRequest("Only .png and .jpg images are allowed!");

            if (dto.Poster.Length > _MaxAllowedPosterSize)
                return BadRequest("Max allowed size for poster is 1MB!");

            using var datastream = new MemoryStream();
            await dto.Poster.CopyToAsync(datastream);

            var movie = _mapper.Map<_Movie>(dto);
            movie.Poster = datastream.ToArray();

            await _movieServices.CreateMovie(movie);
            return Ok(movie);
        }


        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var movies = await _movieServices.GetAllMovies();
            var data = _mapper.Map<IEnumerable<MovieDetailsDto>>(movies);
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var dto = await _movieServices.GetMovieById(id);

            if (dto == null)
                return NotFound("Movie not found!");
            var data = _mapper.Map<MovieDetailsDto>(dto);

            return Ok(data);
        }

        [HttpGet(template:"Genre/{genreId}")]
        public async Task<IActionResult> GetByGenreId(byte genreId)
        {

            var dto = await _genreServices.GetGenreById(genreId);

            if (dto == null)
                return NotFound("Genre not found!");

            var movies = await _movieServices.GetAllMovies(genreId);


            if (!movies.Any())
                return NotFound("No movies found for this genre!");
            var data = _mapper.Map<IEnumerable<MovieDetailsDto>>(movies);

            return Ok(data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var movie = await _movieServices.GetMovieById(id);

            if (movie == null)
                return NotFound("Movie not found!");

            await _movieServices.DeleteMovie(id);
            return Ok("Movie deleted successfully!");   
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] MoviesDto dto)
        {
            var movie = await _movieServices.GetMovieById(id);

            if (movie == null)
                return NotFound("Movie not found!");

            var IsValidGenre = await _genreServices.GetGenreById(dto.GenreId) != null;
            if (!IsValidGenre)
                return BadRequest("Invalid Genre Id!");


            if (dto.Poster != null)
            {
                if (!_AllowedExtention.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
                    return BadRequest("Only .png and .jpg images are allowed!");
                if (dto.Poster.Length > _MaxAllowedPosterSize)
                    return BadRequest("Max allowed size for poster is 1MB!");
                using var datastream = new MemoryStream();
                await dto.Poster.CopyToAsync(datastream);
                movie.Poster = datastream.ToArray();
            }


            movie.Title = dto.Title;
            movie.year = dto.year;
            movie.GenreId = dto.GenreId;
            movie.Rate = dto.Rate;
            movie.Description = dto.Description;
          
            await _movieServices.UpdateMovie(movie);
            return Ok("Movie updated successfully!");
        }

    }
}
