using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Data
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
