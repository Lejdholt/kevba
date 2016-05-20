using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly IExampleClass exampleClass;


        // GET api/values
        public ValuesController(IExampleClass exampleClass)
        {
            this.exampleClass = exampleClass;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { exampleClass.DoShit() + "1", exampleClass.DoShit() + "2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return exampleClass.DoShit() + id;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
