using System;

namespace MovieModels.SearchResults
{
    public class MovieSearchResultInfo
    {
        public string[] Directors { get; set; }

        public DateTime ReleaseDate { get; set; }

        public float? Rating { get; set; }

        public string[] Genres { get; set; }

        public string ImageUrl { get; set; }

        public string Plot { get; set; }

        public int? Rank { get; set; }

        public int RunTimeSec { get; set; }

        public string[] Actors { get; set; }
    }
}
