using Web;
using Web.Controllers;
using Xunit;

namespace Tests
{
    public class MyClass
    {
        [Fact]
        public async void lolz()
        {
            var lol = new MoviesController();
       var list =     await lol.Get();
        }
    }
}