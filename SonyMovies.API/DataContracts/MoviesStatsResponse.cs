using Newtonsoft.Json;

namespace SonyMovies.API.DataContracts
{
    public class MoviesStatsResponse
    {
        [JsonProperty("movieId")]
        public int MovieId { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("averageWatchDurationS")]
        public int AverageWatchDuration { get; set; }

        [JsonProperty("watches")]
        public int Watches { get; set; }

        [JsonProperty("releaseYear")]
        public int ReleaseYear { get; set; }
    }
}
