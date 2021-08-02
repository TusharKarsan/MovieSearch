using System.Collections.Generic;

namespace MovieModels.MoviePoco
{
    public class MovieComparer : IEqualityComparer<Movie>
    {
        public bool Equals(Movie x, Movie y)
        {
            return (x.Title == y.Title) && (x.Year == y.Year); // is not case insensitive?
        }

        public int GetHashCode(Movie obj)
        {
            return obj.Title.GetHashCode() ^ obj.Year.ToString().GetHashCode();
        }
    }
}
