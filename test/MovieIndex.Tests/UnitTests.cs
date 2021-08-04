using MovieModels.MoviePoco;
using NUnit.Framework;
using FluentAssertions;

namespace MovieIndex.Tests
{
    public class Tests
    {

        private readonly IMovieIndex _sut = new MovieIndex();

        /// <summary>
        /// Note that Setup cannot be used due to the use of static data. Index is build once int he static method.
        /// </summary>
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Movie[] movies = new Movie[] {
                new Movie { Title="title1a and title1b", Year=1000, Info = new MovieInfo { Genres = new[] { "genre1a", "genre1b", "genre1a" } } },
                new Movie { Title="title1a the title2b", Year=1001, Info = new MovieInfo { Genres = new[] { "genre1a", "genre2b" } } },
                new Movie { Title="title1a in  title3b", Year=1002, Info = new MovieInfo { Genres = new[] { "genre1a", "genre3b" } } }
            };

            _sut.BuildIndex(movies);
        }

        [Test]
        public void AfterIndexIsBuilt_IndexHasExpectedYears()
        {
            var result = _sut.GetYears();

            result.Should().Contain(new[] { 1000, 1001, 1002 });

            result.Length.Should().Be(3);
        }

        [Test]
        public void AfterIndexIsBuilt_IndexHasExpectedGenres()
        {
            var result = _sut.GetGenres();

            result.Should().Contain(new[] { "genre1a", "genre1b", "genre2b", "genre3b" });

            result.Length.Should().Be(4);
        }

        [Test]
        public void AfterIndexIsBuilt_IndexHasExpectedWords()
        {
            var result = _sut.GetWords();

            result.Should().Contain(new[] { "title1a", "title1b", "title2b", "title3b" });

            result.Length.Should().Be(4);
        }

        [Test]
        [TestCase("Find me not", null, null, 0)]
        [TestCase("title1a", null, null, 3)]
        [TestCase("title2b", null, null, 1)]
        [TestCase("titl title2b", null, null, 1)]
        [TestCase("title1a", null, new[] { "genre2b" }, 1)]
        [TestCase("title1a", new[] { 1001 } , null, 1)]
        [TestCase("title1a", new[] { 1002, 9000 }, null, 1)]
        public void AfterIndexIsBuilt_SearchReturnExpectedResults(string terms, int[] years, string[] genres, int expectedResults)
        {
            var result = _sut.Search(terms, years, genres);

            result.Count.Should().Be(expectedResults);
        }
    }
}