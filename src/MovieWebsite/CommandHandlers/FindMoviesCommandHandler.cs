using MovieIndex;
using MovieModels.MoviePoco;
using System.Collections.Generic;

namespace MovieWebsite.CommandHandlers
{
    public interface IFindMoviesCommandHandlers
    {
        List<Movie> Handle(string search, string[] genres, int[] years);
    }

    public class FindMoviesCommandHandler : IFindMoviesCommandHandlers
    {
        private readonly IMovieIndex _movieIndex;

        public FindMoviesCommandHandler(IMovieIndex movieIndex)
        {
            _movieIndex = movieIndex;
        }

        public List<Movie> Handle(string search, string[] genres, int[] years)
        {
            return _movieIndex.Search(search, years, genres);
        }
    }
}
