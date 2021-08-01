using MovieDB.Models;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace MovieDB
{
    public class MovieData
    {
        public static Movie[] Movies { get; private set; }

        static MovieData()
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
            
            string exeDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string dataFilePath = Path.Combine(exeDirectory, "MovieData.json");

            var json = File.ReadAllText(dataFilePath, Encoding.UTF8);
            Movies = JsonSerializer.Deserialize<Movie[]>(json, options);
        }
    }
}
