using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Web.Services;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Web.API
{
    [Route("api/[controller]")]
    public class MovieController : Controller
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
