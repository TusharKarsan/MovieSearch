using AutoMapper;
using MovieDB.Models;
using MovieModels.MoviePoco;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace MovieDB
{

    public class MovieData : IMovieData
    {
        private readonly IMapper _mapper;
        private static DbMovie[] _movies;

        public MovieData(IMapper mapper)
        {
            _mapper = mapper;
        }

        static MovieData()
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };

            string exeDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string dataFilePath = Path.Combine(exeDirectory, "MovieData.json");

            var json = File.ReadAllText(dataFilePath, Encoding.UTF8);
            _movies = JsonSerializer.Deserialize<DbMovie[]>(json, options);
        }

        public Movie[] GetMovies()
        {
            return _mapper.Map<Movie[]>(_movies);
        }
    }
}
