namespace MovieModels.MoviePoco
{
    public class Movie
    {
        public int Id { get; set; }

        public int Year { get; set; }

        public string Title { get; set; }

        public MovieInfo Info { get; set; }
    }
}
