using System.Collections.Generic;
using System.Linq;
using Neo4j.Driver.V1;

namespace Data
{

    public class MovieService
    {
        public MovieService()
        {

        }

        public void AddMovies(IEnumerable<Movie> movies)
        {
            using (var driver = GraphDatabase.Driver("bolt://localhost:7474/", AuthTokens.Basic("neo4j", "qqj4ab")))
            using (var session = driver.Session())
            {
                foreach (var movie in movies)
                {
                    var strippedMovie = movie.Title.Replace(" ", string.Empty);
                    session.Run($"CREATE ({strippedMovie}:Movie {{title:'{movie.Title}', releaseDate:'{movie.ReleaseDate.ToString("o")}',imageUrl:'{movie.ImageUrl}'}})");

                    foreach (var actor in movie.Actors)
                    {
                        var strippedActor = actor.Name.Replace(" ", string.Empty);

                        var result = session.Run($"MATCH ({strippedActor}:Person) WHERE a.name = '{actor.Name}' RETURN {strippedActor}");

                        if (!result.Any())
                        {
                            session.Run($"CREATE ({strippedActor}:Person {{name:'{actor.Name}', imageUrl:'{actor.ImageUrl}'}})");
                        }

                        session.Run($"CREATE ({strippedActor}) -[:ACTED_IN]-> ({strippedMovie})");
                    }
                }
            }
        }
    }
}
