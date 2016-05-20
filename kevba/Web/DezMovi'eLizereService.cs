using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;

namespace Web
{
    public class Service
    {
        public async void motherfucker()
        {
            string result;
            using (var client = new HttpClient())
            {
                result = await client.GetStringAsync("https://api.themoviedb.org/3/movie/55?api_key=ef790bbc30e5be2b19e777aef1d8c488");
            }
            var oneMovie = JsonConvert.DeserializeObject<Result>(result);

        }
    }

    public class Result
    {
        public bool adult { get; set; }
        public string backdrop_path { get; set; }
        public int id { get; set; }
        public string original_title { get; set; }
        public string release_date { get; set; }
        public string poster_path { get; set; }
        public double popularity { get; set; }
        public string title { get; set; }
        public bool video { get; set; }
        public double vote_average { get; set; }
        public int vote_count { get; set; }
    }

    public class RootObject
    {
        public int page { get; set; }
        public List<Result> results { get; set; }
        public int total_pages { get; set; }
        public int total_results { get; set; }
    }
}
