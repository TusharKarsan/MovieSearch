using AutoMapper;
using MovieModels.MoviePoco;
using MovieModels.SearchResults;

namespace MovieWebsite
{
    public class MovieWebsiteMapperProfile : Profile
    {
        public MovieWebsiteMapperProfile()
        {
            CreateMap<MovieInfo, MovieSearchResultInfo>();
            CreateMap<Movie, MovieSearchResult>();
        }
    }
}
