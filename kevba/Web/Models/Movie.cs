using System;

namespace Web.Models
{
    public class Movie
    {
        public string id { get; set; }
        public string original_title { get; set; }
        public DateTime release_date { get; set; }
        public string poster_path { get; set; }
        public string title { get; set; }
    }
}