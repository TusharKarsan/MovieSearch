namespace MovieModels.SearchResults
{
    public class MovieSearchResult
    {
        public int Id { get; set; }

        public int Year { get; set; }

        public string Title { get; set; }

        public MovieSearchResultInfo Info { get; set; }
    }
}
