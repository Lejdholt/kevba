using System;
using System.Collections.Generic;

namespace Data
{
    public class Movie
    {
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string ImageUrl { get; set; }
        public IEnumerable<Actor> Actors { get; set; }
    }
}