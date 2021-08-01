using System;
using MovieDB;

namespace MovieConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var movies = MovieData.Movies;
            Console.WriteLine($"Movies read {movies.Length}");

            Console.Write("Hit [Enter] to exist: ");
            Console.ReadLine();
        }
    }
}
