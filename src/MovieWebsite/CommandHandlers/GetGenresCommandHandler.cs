using MovieIndex;

namespace MovieWebsite.CommandHandlers
{
    public interface IGetGenresCommandHandler
    {
        string[] Handle();
    }

    public class GetGenresCommandHandler : IGetGenresCommandHandler
    {
        private readonly IMovieIndex _movieIndex;

        public GetGenresCommandHandler(IMovieIndex movieIndex)
        {
            _movieIndex = movieIndex;
        }

        public string[] Handle()
        {
            return _movieIndex.GetGenres();
        }
    }
}
