using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            using (var driver = GraphDatabase.Driver("bolt://localhost/", AuthTokens.Basic("neo4j", "qqj4ab")))
            using (var session = driver.Session())
            {
                var sb = new StringBuilder();

                var tMovies = movies.Select(movie => new {dbName = movie.Title.Replace(" ", string.Empty), movie.Title, ReleaseDate = movie.ReleaseDate.ToString("o"), movie.ImageUrl, Actors = movie.Actors.Select(a => new {dbName = a.Name.Replace(" ", string.Empty), a.Name, a.ImageUrl})}).ToArray();

                var actorCommands = tMovies.SelectMany(m => m.Actors).Select(actor => $"MERGE ({actor.dbName}:Person {{name:'{actor.Name}', imageUrl:'{actor.ImageUrl}'}})").Distinct();
                var movieCommands = tMovies.Select(movie => $"MERGE ({movie.dbName}:Movie {{title:'{movie.Title}', releaseDate:'{movie.ReleaseDate}',imageUrl:'{movie.ImageUrl}'}})");
                var actedinCommands = tMovies.SelectMany(movie => movie.Actors, (movie, actor) => $" MERGE ({actor.dbName}) -[:ACTED_IN]-> ({movie.dbName})").ToList();

                foreach (var cmd in actorCommands)
                {
                    sb.AppendLine(cmd);
                }
                foreach (var cmd in movieCommands)
                {
                    sb.AppendLine(cmd);
                }
                foreach (var cmd in actedinCommands)
                {
                    sb.AppendLine(cmd);
                }

                session.Run(sb.ToString());
            }
        }


        public ActorConnection GetConnection(string actorFrom, string actorTo)
        {
            using (var driver = GraphDatabase.Driver("bolt://localhost/", AuthTokens.Basic("neo4j", "qqj4ab")))
            using (var session = driver.Session())
            {
                IRecord result = session.Run($"MATCH p=shortestPath( (from:Person {{name:\"{actorFrom}\"}})-[*]-(to:Person {{name:\"{actorTo}\"}}) ) RETURN p").Single();
                var path = result["p"] as IPath;
                IReadOnlyList<INode> readOnlyList = path.Nodes;
                return DoShit(readOnlyList);
            }
        }

        public ActorConnection DoShit(IEnumerable<INode> nodes)
        {
            var node = nodes.FirstOrDefault();

            if (node == null)
            {
                return null;
            }

            return new ActorConnection
                   {
                       ActorName = node["name"].As<string>(),
                       ImageUrl = node["imageUrl"].As<string>(),
                       Movie = DoShit2(nodes.Except(new List<INode> {node}))
                   };
        }

        public MovieConnection DoShit2(IEnumerable<INode> nodes)
        {
            var node = nodes.FirstOrDefault();

            if (node == null)
            {
                return null;
            }

            return new MovieConnection
                   {
                       MovieTitle = node["title"].As<string>(),
                       ImageUrl = node["imageUrl"].As<string>(),
                       Actor = DoShit(nodes.Except(new List<INode> {node}))
                   };
        }
    }
}