using MovieModels.MoviePoco;
using System.Collections.Generic;

namespace MovieWebsite.Models
{
    public class HomeIndexViewModel
    {
        public Movie[] Movies { get; private set; }

        public HomeIndexViewModel(List<Movie> movies)
        {
            Movies = movies.ToArray();
        }
    }
}
