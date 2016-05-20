using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json;

namespace Web.Controllers
{
    [RoutePrefix("api/[controller]")]
    public class MoviesController : ApiController
    {
        [Route("amovie")]
        [HttpGet]
        private async Task<Result> GetExternalResponse()
        {
            string result;
            using (var client = new HttpClient())
            {
                result = await client.GetStringAsync("https://api.themoviedb.org/3/movie/55?api_key=ef790bbc30e5be2b19e777aef1d8c488");
            }
            return JsonConvert.DeserializeObject<Result>(result);
        }
    }
}
