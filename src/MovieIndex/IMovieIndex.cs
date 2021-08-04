using MovieModels.MoviePoco;
using System.Collections.Generic;

namespace MovieIndex
{
    public interface IMovieIndex
    {
        int[] GetYears();

        string[] GetGenres();

        string[] GetWords();

        void BuildIndex(Movie[] movies);

        List<Movie> Search(string searchTerms, int[] years, string[] genres = null);
    };
}
