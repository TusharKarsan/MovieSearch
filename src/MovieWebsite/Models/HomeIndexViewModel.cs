using MovieModels.MoviePoco;
using System.Collections.Generic;

namespace MovieWebsite.Models
{
    public class HomeIndexViewModel
    {
        public List<Movie> Movies { get; private set; }

        public HomeIndexViewModel(List<Movie> movies)
        {
            Movies = movies;
        }
    }
}
