using AutoMapper;
using MovieDB.Models;
using MovieModels;

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
