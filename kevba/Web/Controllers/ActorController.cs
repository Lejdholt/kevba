using System.Web.Http;
using Data;

namespace Web.Controllers
{
    [RoutePrefix("api/actor")]
    public class ActorController : ApiController
    {
        [HttpGet]
        [Route("connect/{from}/{to}")]
        public ActorConnection Get(string from,string to)
        {
            var service = new MovieService();

            return service.GetConnection(from, to);
        }
    }
}