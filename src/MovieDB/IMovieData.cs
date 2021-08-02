using MovieModels.MoviePoco;

namespace MovieDB
{
    public interface IMovieData
    {
        Movie[] GetMovies();
    }
}
