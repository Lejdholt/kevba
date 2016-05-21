using System;
using System.Collections.Generic;

namespace Data
{
    public class Movie
    {
        private string original_title;
        private DateTime release_date;
        private string poster_path;
        private List<Actor> list;

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

        public Movie(string original_title, DateTime release_date, string poster_path, List<Actor> list)
        {
            this.original_title = original_title;
            this.release_date = release_date;
            this.poster_path = poster_path;
            this.list = list;
        }
    }
}