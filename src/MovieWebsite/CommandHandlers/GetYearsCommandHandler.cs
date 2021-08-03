using MovieIndex;

namespace MovieWebsite.CommandHandlers
{
    public interface IGetYearsCommandHandler
    {
        int[] Handle();
    }

    public class GetYearsCommandHandler : IGetYearsCommandHandler
    {
        private readonly IMovieIndex _movieIndex;

        public GetYearsCommandHandler(IMovieIndex movieIndex)
        {
            _movieIndex = movieIndex;
        }

        public int[] Handle()
        {
            return _movieIndex.GetYears();
        }
    }
}
