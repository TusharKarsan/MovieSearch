using MovieModels.MoviePoco;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MovieIndex
{
    public class MovieIndex : IMovieIndex
    {
        private static bool _indexedAlready = false;

        private static readonly SortedList<int, List<Movie>> _yearToMovie;

        private static readonly SortedList<string, List<Movie>> _genreToMovie;

        private static readonly SortedList<string, List<Movie>> _wordToMovie;

        static MovieIndex()
        {
            var descendingComparer = Comparer<int>.Create((x, y) => y.CompareTo(x));

            _yearToMovie = new SortedList<int, List<Movie>>(descendingComparer);
            _genreToMovie = new SortedList<string, List<Movie>>();
            _wordToMovie = new SortedList<string, List<Movie>>();
        }

        public int[] GetYears() => _yearToMovie.Keys.ToArray();

        public string[] GetGenres() => _genreToMovie.Keys.ToArray();

        public string[] GetWords() => _wordToMovie.Keys.ToArray();

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
                throw new InvalidOperationException("Multiple index builds is not supported!");
            _indexedAlready = true;

            foreach (Movie movie in movies)
            {
                if (movie.Year > 0)
                {
                    if (_yearToMovie.ContainsKey(movie.Year))
                        _yearToMovie[movie.Year].Add(movie);
                    else
                        _yearToMovie.Add(movie.Year, new List<Movie> { movie });
                }

                if(movie.Info.Genres != null)
                {
                    foreach(string genre in movie.Info.Genres) // assumes there are no duplicates in the array
                    {
                        var genreLower = genre.Trim().ToLower();

                        if (_genreToMovie.ContainsKey(genreLower))
                            _genreToMovie[genreLower].Add(movie);
                        else
                            _genreToMovie.Add(genreLower, new List<Movie> { movie });
                    }
                }

                var terms = SplitAndSanitize(movie.Title);

                foreach (string term in terms)
                {
                    if (_wordToMovie.ContainsKey(term))
                        _wordToMovie[term].Add(movie);
                    else
                        _wordToMovie.Add(term, new List<Movie> { movie });
                }
            }
        }

        private static char[] _separators = { ' ', '\t', '\n', '\r', ',', '.', '?', '&', '-', '+', ':' };

        private static string[] _stopWrods = { "a", "and", "of", "in", "the" };

        public static List<string> SplitAndSanitize(string terms)
        {
            if (terms == null)
                return new List<string>();

            var split = terms.Split(_separators, StringSplitOptions.RemoveEmptyEntries);

            return split.Except(_stopWrods, StringComparer.InvariantCultureIgnoreCase).Select(term => term.ToLower()).ToList();
        }

        public List<Movie> Search(string searchTerms, int[] years, string[] genres = null)
        {

            if (string.IsNullOrWhiteSpace(searchTerms))
                throw new InvalidOperationException("Search term is required");

            List<Movie> resultYears = SearchYears(years);

            List<Movie> resultGenres = SearchGenres(genres);

            List<Movie> result = SearchTerms(searchTerms);

            if (years?.Length > 0)
            {
                result = result.Intersect(resultYears).ToList();

                if (genres?.Length > 0)
                {
                    result = result.Intersect(resultGenres).ToList();
                }
            }
            else
            {
                if (genres?.Length > 0)
                {
                    result = result.Intersect(resultGenres).ToList();
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

        protected List<Movie> SearchYears(int[] years)
        {
            List<Movie> result = new List<Movie>();

            if (years == null || years.Length == 0)
                return result;

            foreach(int year in years)
            {
                if (_yearToMovie.ContainsKey(year))
                    result = result.Union(_yearToMovie[year]).ToList();
            }

            return result;
        }

        protected List<Movie> SearchGenres(string[] genres)
        {
            List<Movie> result = new List<Movie>();

            if (genres == null || genres.Length == 0)
                return result;

            foreach (string genre in genres)
            {
                var genreLower = genre.Trim().ToLower();

                if (_genreToMovie.ContainsKey(genreLower))
                    result = result.Union(_genreToMovie[genreLower]).ToList();
            }

            return result;
        }

        protected List<Movie> SearchTerms(string searchTerms)
        {
            var result = new List<Movie>();

            var terms = SplitAndSanitize(searchTerms); // It handles NULLs

            var termsInIndex = _wordToMovie.Keys;

            bool isFirstTerm = true;
            foreach (string term in terms)
            {
                List<Movie> termResult = new List<Movie>();

                foreach (string word in termsInIndex)
                {
                    if (word.StartsWith(term))
                    {
                        var movies = _wordToMovie[word];
                        termResult = termResult.Union(movies).ToList();
                    }
                }

                if (isFirstTerm)
                    result = termResult;
                else
                    result = result.Intersect(termResult).ToList();

                isFirstTerm = false;
            }

            return result;
        }
    }
}
