namespace MovieDB.Models
{
    public class Movie
    {
        public int Year { get; set; }

        public string Title { get; set; }

        public MovieInfo Info { get; set; }
    }
}
