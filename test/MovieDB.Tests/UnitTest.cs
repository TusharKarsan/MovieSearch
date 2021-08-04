using AutoFixture;
using AutoMapper;
using Moq;
using MovieDB.Models;
using MovieModels.MoviePoco;
using NUnit.Framework;
using FluentAssertions;

namespace MovieDB.Tests
{
    public class Tests
    {
        private IFixture _fixture;

        private readonly Mock<IMapper> _mockMapper;

        private IMovieData _sut;

        private Movie[] _movies = new[]
        {
                new Movie { Title="title1a and title1b", Year=1000, Info = new MovieInfo { Genres = new[] { "genre1a", "genre1b", "genre1a" } } },
                new Movie { Title="title1a the title2b", Year=1001, Info = new MovieInfo { Genres = new[] { "genre1a", "genre2b" } } },
                new Movie { Title="title1a in  title3b", Year=1002, Info = new MovieInfo { Genres = new[] { "genre1a", "genre3b" } } }
        };

        public Tests()
        {
            _mockMapper = new Mock<IMapper>();

            _mockMapper
                .Setup(x => x.Map<Movie[]>(It.IsAny<DbMovie[]>()))
                .Returns(_movies);

            _sut = new MovieData(_mockMapper.Object);
        }

        [SetUp]
        public void SetUp()
        {
            _fixture = new Fixture(); // this needs work
        }

        [Test]
        public void When_GetMovies_Is_Called_Then_AutoMapper_IsCalled()
        {
            var result = _sut.GetMovies();

            result.Should().HaveCount(3);

            _mockMapper.Verify(x => x.Map<Movie[]>(It.IsAny<DbMovie[]>()), Times.Once);
        }
    }
}