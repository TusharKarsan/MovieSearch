namespace MovieDB.Models
{
    public class DbMovie
    {
        public int Id { get; set; }

        public int Year { get; set; }

        public string Title { get; set; }

        public DbMovieInfo Info { get; set; }
    }
}
