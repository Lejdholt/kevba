using System;
using System.Collections.Generic;

namespace Data
{
    public class Movie
    {
        public string Title { get;  }
        public DateTime ReleaseDate { get;  }
        public string ImageUrl { get;  }
        public IEnumerable<Actor> Actors { get;  }

        public Movie(string title, DateTime releaseDate, string imageUrl, IEnumerable<Actor> actors)
        {
            Title = title;
            ReleaseDate = releaseDate;
            ImageUrl = imageUrl;
            Actors = actors;
        }
    }
}