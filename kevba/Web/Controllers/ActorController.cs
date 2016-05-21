using System.Web.Http;
using Data;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    public class ActorController : ApiController
    {
        [HttpGet]
        public ActorConnection GetConnection(string from, string to)
        {
            var service = new MovieService();

            return service.GetConnection(from, to);
        }
    }
}