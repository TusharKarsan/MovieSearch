using MovieDB;
using MovieIndex;
using MovieModels;
using MovieModels.MoviePoco;
using System;

namespace MovieConsole
{
    public class StartUpConsole : IStartUpConsole
    {
        private readonly AppSettings _appSettings;
        private readonly IMovieData _moveiData;

        public StartUpConsole(
            AppSettings appSettings,
            IMovieData movieData
        )
        {
            _appSettings = appSettings;
            _moveiData = movieData;
        }

        public /*async Task*/ void Run()
        {
            var movies = _moveiData.GetMovies();
            Console.WriteLine($"Movies read {movies.Length}");

            var movieIndexBuilder = new MovieIndex.MovieIndex();
            movieIndexBuilder.BuildIndex(movies);

            Search(movieIndexBuilder, "Terminat");
            Search(movieIndexBuilder, "Term Salv");
            Search(movieIndexBuilder, "New");

            Console.Write("Hit [Enter] to exist: ");
            Console.ReadLine();
        }

        private static void Search(MovieIndex.MovieIndex movieIndexBuilder, string terms)
        {
            Console.WriteLine();
            Console.WriteLine($"Searching '{terms}'");

            var searchResult = movieIndexBuilder.Search(terms);
            Console.WriteLine($"Found {searchResult.Count} movies");

            foreach (Movie movie in searchResult)
            {
                Console.WriteLine($"\t{movie.Info.Rank}\t{movie.Year}\t{movie.Title}");
            }
        }
    }
}
