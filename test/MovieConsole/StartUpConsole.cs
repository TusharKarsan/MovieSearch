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
        private readonly IMovieIndex _movieIndex;

        public StartUpConsole(
            AppSettings appSettings,
            IMovieData movieData,
            IMovieIndex movieIndex
        )
        {
            _appSettings = appSettings;
            _moveiData = movieData;
            _movieIndex = movieIndex;
        }

        public /*async Task*/ void Run()
        {
            var movies = _moveiData.GetMovies();
            Console.WriteLine($"Movies read {movies.Length}");

            _movieIndex.BuildIndex(movies);

            Search("Terminat");
            Search("Term Salv");
            Search("New");
            Search("New York");
            Search("new york", 1990);
            Search("new york gang");

            Console.Write("Hit [Enter] to exist: ");
            Console.ReadLine();
        }

        private void Search(string terms, int year = -1)
        {
            Console.WriteLine();
            Console.WriteLine($"Searching '{terms}' year {year}");

            var searchResult = _movieIndex.Search(terms, year);
            Console.WriteLine($"Found {searchResult.Count} movies");

            foreach (Movie movie in searchResult)
            {
                Console.WriteLine($"\t{movie.Info.Rank}\t{movie.Year}\t{movie.Title}");
            }
        }
    }
}
