using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using SonyMovies.API.DataContracts;

namespace SonyMovies.API.Controllers
{
    /// <summary>
    /// Movie service API
    /// </summary>
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        /// <summary>
        /// Saves a new piece of movie
        /// </summary>
        /// <param name="request">Model containing movie details to be added e.g. movieId, title, language, duration, releaseYear</param>
        /// <returns>A list of contacts matching search criteria provided or a not found response</returns>
        [HttpPost]
        [Route("metadata")]
        [Produces("application/json")]
        public async void SaveNewMovieAsync(MovieDetailsRequest request)
        {
            await _movieService.SaveNewMovieAsync(request);
        }

        /// <summary>
        /// Returns the movie details for a given movie id
        /// </summary>
        /// <param name="request">movieId</param>
        /// <returns>A list of movies matching search criteria provided for a Id</returns>
        [HttpGet]
        [Route("metadata/{movieId}")]
        [Produces("application/json")]
        public async Task<IActionResult> SearchMovieByIdAsync(int movieId)
        {
            var result = await _movieService.SearchMovieByIdAsync(movieId);
            if(result == null || result.Count == 0)
            {
                return NotFound();
            }
            return Ok(result);
        }

        /// <summary>
        /// Returns the statistics for all movies
        /// </summary>
        /// <returns>A list of movies' statistics</returns>
        [HttpGet]
        [Route("movies/stats")]
        [Produces("application/json")]
        public async Task<IActionResult> GetMoviesStatisticsAsync()
        {
            var result = await _movieService.GetMoviesStatisticsAsync();
            if (result == null || result.Count == 0)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
