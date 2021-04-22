namespace SonyMovies.API.DataContracts
{
    public class Movie
    {
        public Movie(int id, int movieId, string title, string language, string duration, int releaseYear)
        {
            Id = id;
            MovieId = movieId;
            Title = title;
            Language = language;
            Duration = duration;
            ReleaseYear = releaseYear;
        }

        public Movie()
        {

        }

        public int Id { get; set; }

        public int MovieId { get; set; }

        public string Title { get; set; }

        public string Language { get; set; }

        public string Duration { get; set; }

        public int ReleaseYear { get; set; }
    }
}
