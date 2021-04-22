using System.Collections.Generic;
using System.Threading.Tasks;
using SonyMovies.API.DataContracts;

namespace SonyMovies.API
{
    public interface IMovieService
    {
        Task SaveNewMovieAsync(MovieDetailsRequest request);

        Task<List<MovieDetailsResponse>> SearchMovieByIdAsync(int id);

        Task<List<MoviesStatsResponse>> GetMoviesStatisticsAsync();

    }
}
