using System;
using System.Text.Json.Serialization;

namespace MovieDB.Models
{
    public class MovieInfo
    {
        public string[] Directors { get; set; }

        [JsonPropertyName("release_date")]
        public DateTime ReleaseDate { get; set; }

        public float? Rating { get; set; }

        public string[] Genres { get; set; }

        [JsonPropertyName("image_url")]
        public string ImageUrl { get; set; }

        public string Plot { get; set; }

        public int? Rank { get; set; }

        [JsonPropertyName("running_time_secs")]
        public int RunTimeSec { get; set; }

        public string[] Actors { get; set; }
    }
}
