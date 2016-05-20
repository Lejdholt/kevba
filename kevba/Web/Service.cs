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
}