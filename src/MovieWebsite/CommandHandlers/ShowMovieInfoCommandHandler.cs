using MovieDB;
using MovieModels.MoviePoco;
using System.Linq;

namespace MovieWebsite.CommandHandlers
{
    public interface IShowMovieInfoCommandHandler
    {
        Movie Handle(int movieId);
    }

    public class ShowMovieInfoCommandHandler : IShowMovieInfoCommandHandler
    {
        private readonly IMovieData _moveiData;

        public ShowMovieInfoCommandHandler(IMovieData movieData)
        {
            _moveiData = movieData;
        }

        public Movie Handle(int movieId)
        {
            var movies = _moveiData.GetMovies();

            return movies.FirstOrDefault(movie => movie.Id == movieId);
        }
    }
}
