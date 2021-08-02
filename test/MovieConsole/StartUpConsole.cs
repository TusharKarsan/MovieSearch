using MovieDB;
using MovieModels;
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

            Console.Write("Hit [Enter] to exist: ");
            Console.ReadLine();
        }
    }
}
