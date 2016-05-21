﻿using System;
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
            var sut = new MovieService();
            sut.AddMovies(new List<Movie>()
                          {
                              new Movie(
                                  imageUrl: "http://ia.media-imdb.com/images/M/MV5BODE1MDczNTUxOV5BMl5BanBnXkFtZTcwMTA0NDQyNA@@._V1_UX182_CR0,0,182,268_AL_.jpg",
                                  releaseDate : new DateTime(1984,10,26),
                                  title :"The Terminator",
                                  actors: new List<Actor>
                                          {
                                                         new Actor(
                                                              "Arnold Schwarzenegger",
                                                              "http://ia.media-imdb.com/images/M/MV5BMTI3MDc4NzUyMV5BMl5BanBnXkFtZTcwMTQyMTc5MQ@@._V1_UY317_CR19,0,214,317_AL_.jpg"
                                                         )
                                                     })
                          });
        }
    }
}
