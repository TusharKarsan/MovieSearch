using MovieModels.MoviePoco;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MovieIndex
{
    public interface IMovieIndexBuilder
    {
        void BuildIndex(Movie[] movies);

        List<Movie> Search(string searchTerms, int year = -1);
    };

    public class MovieIndex : IMovieIndexBuilder
    {
        private static bool _indexedAlready = false;

        private static readonly SortedList<int, List<Movie>> _yearToMovie;

        private static readonly SortedList<string, List<Movie>> _wordToMovie;

        static MovieIndex()
        {
            _yearToMovie = new SortedList<int, List<Movie>>();
            _wordToMovie = new SortedList<string, List<Movie>>();
        }

        public void BuildIndex(Movie[] movies)
        {
            if (movies == null)
                return;

            /*
             * The following for-each is not checking the list of movies
             * already in the list and assumes and each eteration will
             * result in a unique movie. So the build method cannot be
             * called more than once.
             */

            if (_indexedAlready)
                throw new InvalidOperationException("Multiple int builds is not supported!");
            _indexedAlready = true;

            foreach(Movie movie in movies)
            {
                if (movie.Year > 0)
                {
                    if (_yearToMovie.ContainsKey(movie.Year))
                        _yearToMovie[movie.Year].Add(movie);
                    else
                        _yearToMovie.Add(movie.Year, new List<Movie> { movie });
                }

                var terms = SplitAndSanitize(movie.Title);

                foreach(string term in terms)
                {
                    if (_wordToMovie.ContainsKey(term))
                        _wordToMovie[term].Add(movie);
                    else
                        _wordToMovie.Add(term, new List<Movie> { movie });
                }
            }
        }

        private static char[] _separators = { ' ', '\t', '\n', '\r', ',', '.', '?', '&', '-', '+', ':' };

        private static string[] _stopWrods = { "a", "and", "the" };

        public static List<string> SplitAndSanitize(string terms)
        {
            if (terms == null)
                return new List<string>();

            var split = terms.Split(_separators, StringSplitOptions.RemoveEmptyEntries);

            return split.Except(_stopWrods, StringComparer.InvariantCultureIgnoreCase).Select(term => term.ToLower()).ToList();
        }

        public List<Movie> Search(string searchTerms, int year = -1)
        {
            var result = (year > 0 && _yearToMovie.ContainsKey(year)) ? _yearToMovie[year] : new List<Movie>();

            var terms = SplitAndSanitize(searchTerms); // It handles NULLs

            if (terms.Count == 0)
                return result;

            var termsInIndex = _wordToMovie.Keys;

            foreach(string word in termsInIndex)
            {
                foreach (string term in terms)
                {
                    if (word.StartsWith(term))
                    {
                        var movies = _wordToMovie[word];
                        result = result.Union(movies).ToList(); // Need object ref, not MovieComparer
                    }
                }
            }

            result.Sort(delegate (Movie x, Movie y)
            {
                int rankX = x.Info.Rank ?? 0;
                int rankY = y.Info.Rank ?? 0;

                return rankY - rankX;
            });

            return result;
        }
    }
}
