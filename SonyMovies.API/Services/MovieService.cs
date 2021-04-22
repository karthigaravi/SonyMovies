using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Globalization;
using System.Threading.Tasks;
using SonyMovies.API.DataContracts;

namespace SonyMovies.API
{
    public class MovieService : IMovieService
    {
        public async Task SaveNewMovieAsync(MovieDetailsRequest request)
        {
            
            try
            {
                var config = new CsvConfiguration(CultureInfo.InvariantCulture) { HasHeaderRecord = true, HeaderValidated = null, MissingFieldFound = null, IgnoreBlankLines = false };

                int id;
                using (var reader = new StreamReader("metadataNew.csv"))
                using (var csv = new CsvReader(reader, config))
                {
                    id = csv.GetRecords<Movie>().OrderByDescending(x => x.Id).FirstOrDefault().Id + 1;

                }

                Movie movie = new Movie(id, request.MovieId, request.Title, request.Language, request.Duration, request.ReleaseYear);

                using (var writer = new StreamWriter("metadataNew.csv", true))
                using (var csv = new CsvWriter(writer, config))
                {
                    csv.NextRecord();
                    csv.WriteRecord(movie);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<MovieDetailsResponse>> SearchMovieByIdAsync(int id)
        {
            List<MovieDetailsResponse> result;

            try
            {
                var config = new CsvConfiguration(CultureInfo.InvariantCulture) { HasHeaderRecord = true, HeaderValidated = null, MissingFieldFound = null, IgnoreBlankLines = false };

                using (var reader = new StreamReader("metadata.csv"))
                using (var csv = new CsvReader(reader, config))
                {
                    result = csv.GetRecords<MovieDetailsResponse>().Where(x => x.MovieId == id).GroupBy(x => x.Language).Select(x => x.FirstOrDefault()).ToList();
                                
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return result;
        }

        public async Task<List<MoviesStatsResponse>> GetMoviesStatisticsAsync()
        {
            List<MoviesStatsResponse> result;

            try
            {
                var config = new CsvConfiguration(CultureInfo.InvariantCulture) { HasHeaderRecord = true, HeaderValidated = null, MissingFieldFound = null, IgnoreBlankLines = false };

                List<Movie> movieRecords;

                using (var readerMovies = new StreamReader("metadata.csv"))
                using (var csvMovies = new CsvReader(readerMovies, config))
                {
                    movieRecords = csvMovies.GetRecords<Movie>().ToList();
                }

                List<MovieStats> movieStatsRecords;
                using (var readerStats = new StreamReader("stats.csv"))
                using (var csvStats = new CsvReader(readerStats, config))
                {
                    movieStatsRecords = csvStats.GetRecords<MovieStats>().ToList();
                }

                var list = movieStatsRecords.GroupBy(x => new { x.movieId })
                                 .Select(x => new
                                 {
                                     MovieId = x.Key.movieId,
                                     AverageWatchDurationS = Convert.ToInt32(x.Average(a => a.watchDurationMs) / 1000),
                                     watches = x.Count()
                                 });

                result = (from moviesStat in list
                        join movieRec in movieRecords on moviesStat.MovieId equals movieRec.MovieId
                        select new MoviesStatsResponse()
                        {
                            MovieId = moviesStat.MovieId,
                            Title = movieRec.Title,
                            AverageWatchDuration = moviesStat.AverageWatchDurationS,
                            Watches = moviesStat.watches,
                            ReleaseYear = movieRec.ReleaseYear
                        }).GroupBy(x => x.Title).Select(x => x.First()).OrderByDescending(x => x.Watches).OrderByDescending(x => x.ReleaseYear).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }

            return result;
        }
    }
}
