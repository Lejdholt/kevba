using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class Movie
    {
        public bool Adult { get; set; }
        public string BackdropPath { get; set; }
        public int Id { get; set; }
        public string OriginalTitle { get; set; }
        public string ReleaseDate { get; set; }
        public string PosterPath { get; set; }
        public double Popularity { get; set; }
        public string Title { get; set; }
        public bool Video { get; set; }
        public double VoteAverage { get; set; }
        public int vote_count { get; set; }
    }
}
