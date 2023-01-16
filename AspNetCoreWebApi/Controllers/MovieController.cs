using AspNetCoreWebApi.Models;
using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreWebApi.Controllers
{
    [Route("api.[controller]")]
    [ApiController] //As you see, the class is decorated with the [ApiController] attribute. This attribute indicates that the controller responds to web API requests.
    public class MovieController : ControllerBase
    {
        private readonly MovieContext _dbContext;
        public MovieController(MovieContext dbContext)
        {
            _dbContext = dbContext;
        }

        //GET: api/Movies - Creating Get Method
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        {
            if (_dbContext.Movies == null)
            {
                return NotFound();
            }
            return await _dbContext.Movies.ToListAsync();
        }

        //GET: api/Movies/5
        [HttpGet("{Id}")]
        public async Task<ActionResult<Movie>> GetMovie(int Id)
        {
            if(_dbContext.Movies == null)
            {
                return NotFound();
            }

            var movie = await _dbContext.Movies.FindAsync(Id);

            if (movie == null)
            {
                return NotFound();
            }

            return movie;
        }

        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovie(Movie movie)
        {
            _dbContext.Movies.Add(movie);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetMovie), new { Id = movie.Id}, movie); //Returns an HTTP 201 status code, if successful. HTTP 201 is the standard response for an HTTP POST method that creates a new resource on the server. Adds a Location header to the response.The Location header specifies the URI of the newly created movie record. References the GetMovie action to create the Location header's URI.
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<Movie>> PutMovie (Movie movie, int Id)
        {
            if(Id != movie.Id)
            {
                return BadRequest();
            }

            _dbContext.Entry(movie).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) 
            {
                if (!MovieExists(Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        private bool MovieExists(long Id) 
        {
            return (_dbContext.Movies?.Any(e => e.Id == Id)).GetValueOrDefault();
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult<Movie>> DeleteMovie(int Id)
        {
            if(_dbContext.Movies == null)
            {
                return NotFound();
            }

            var movie = await _dbContext.Movies.FindAsync(Id);
            if(movie == null)
            {
                return NotFound();
            }

            _dbContext.Movies.Remove(movie);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
