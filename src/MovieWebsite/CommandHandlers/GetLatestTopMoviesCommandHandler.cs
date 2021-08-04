using MovieDB;
using MovieModels.MoviePoco;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MovieWebsite.CommandHandlers
{
    public interface IGetLatestTopMoviesCommandHandler
    {
        List<Movie> Handle(int numberOfMovies);
    }

    public class GetLatestTopMoviesCommandHandler : IGetLatestTopMoviesCommandHandler
    {
        private readonly IMovieData _moveiData;

        public GetLatestTopMoviesCommandHandler(IMovieData movieData)
        {
            _moveiData = movieData;
        }

        public List<Movie> Handle(int numberOfMovies)
        {
            var movies = _moveiData.GetMovies().ToList();

            if (movies.Count < numberOfMovies)
                throw new ArgumentOutOfRangeException("not enough movies can be found");

            movies.Sort(delegate (Movie x, Movie y) // This is not the most effecient method.
            {
                int rankX = x.Info.Rank ?? 0;
                int rankY = y.Info.Rank ?? 0;

                if(y.Year == x.Year)
                    return rankY - rankX;

                return y.Year - x.Year;
            });

            return movies.Where(movie => !string.IsNullOrWhiteSpace(movie.Info.ImageUrl)).Take(numberOfMovies).ToList();
        }
    }
}
