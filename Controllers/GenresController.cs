using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace FilmAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenreServices _genreServices;

        public GenresController(IGenreServices genreServices)
        {
            _genreServices = genreServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var genres = await _genreServices.GetAllGenres();
            return Ok(genres);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAsync([FromBody] CreateGenreDto createGenreDto)
        {
            var genre = new Genre
            {
                Name = createGenreDto.Name 
            };
            
            await _genreServices.CreateGenre(genre);
            return Ok(genre);
        }


        [HttpPut(template:"{id}")]
        public async Task<IActionResult> UpdateAsync(byte id, [FromBody] CreateGenreDto updateGenreDto)
        {
            var genre = await _genreServices.GetGenreById(id);
            if (genre == null)
            {
                return NotFound(value:$"this id {id} not found");
            }
            
            genre.Name = updateGenreDto.Name;
            await _genreServices.UpdateGenre(id, genre);
            return Ok(genre);
        }

        [HttpDelete(template: "{id}")]
        public async Task<IActionResult> DeleteAsync(byte id)
        {
            var genre = await _genreServices.GetGenreById(id);
            if (genre == null)
            {
                return NotFound(value: $"this id {id} not found");
            }

            await _genreServices.DeleteGenre(id);
            return Ok(value: $"this id {id} deleted successfully");
        }
    }
}
