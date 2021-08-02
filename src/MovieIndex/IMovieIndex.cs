using MovieModels.MoviePoco;
using System.Collections.Generic;

namespace MovieIndex
{
    public interface IMovieIndex
    {
        void BuildIndex(Movie[] movies);

        List<Movie> Search(string searchTerms, int year = -1);
    };
}
