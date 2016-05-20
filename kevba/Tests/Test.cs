using System.Collections.Generic;
using Data;
using Xunit;

namespace Tests
{
    public class Test
    {
        [Fact]
        public void balls()
        {
           var sut= new MovieService();
            sut.AddMovies(new List<Movie>());
        }
    }
}
