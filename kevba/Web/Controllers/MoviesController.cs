using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Data;
using Newtonsoft.Json;
using Actor = Web.Models.Actor;

namespace Web.Controllers
{
    [RoutePrefix("api/movies")]
    public class MoviesController : ApiController
    {
        [Route("test")]
        [HttpGet]
        public async Task<IEnumerable<Data.Movie>> Get()
        {
            var läggDataHär = new List<Data.Movie>();
            using (var client = new HttpClient())
            {
                var result = await client.GetStringAsync("http://api.themoviedb.org/3/list/522effe419c2955e9922fcf3?api_key=ef790bbc30e5be2b19e777aef1d8c488");

                var deserializeObject = JsonConvert.DeserializeObject<MovieResult>(result);
                foreach (var movie in deserializeObject.Items)
                {
                    result = await client.GetStringAsync($"http://api.themoviedb.org/3/movie/{movie.id}/casts?api_key=ef790bbc30e5be2b19e777aef1d8c488");
                    var item = new Data.Movie(movie.original_title, movie.release_date, movie.poster_path, GetActors(result));
                    läggDataHär.Add(item);
                    Thread.Sleep(300);
                }
            }
            return läggDataHär;
        }

        private static IEnumerable<Data.Actor> GetActors(string result)
        {
            var actorResult = JsonConvert.DeserializeObject<ActorResult>(result);
            foreach (var actor in actorResult.cast)
            {
                yield return new Data.Actor(actor.Name, actor.profile_path);
            }
        }

        public class MovieResult
        {
            public List<Models.Movie> Items { get; set; }
        }

        public class ActorResult
        {
            public List<Actor> cast { get; set; }
        }
    }
}
