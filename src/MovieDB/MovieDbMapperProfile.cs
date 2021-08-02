using AutoMapper;
using MovieDB.Models;
using MovieModels.MoviePoco;

namespace MovieDB
{
    public class MovieDbMapperProfile : Profile
    {
        public MovieDbMapperProfile()
        {
            CreateMap<DbMovieInfo, MovieInfo>();
            CreateMap<DbMovie, Movie>();
        }
    }
}
