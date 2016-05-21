using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Neo4j.Driver.V1;

namespace Data
{
    public static class StringExtensions
    {
        public static string ClearShit(this string value)
        {
            var littleClear = value.Replace(" ", string.Empty)
                        .Replace(".", string.Empty)
                        .Replace(",", string.Empty)
                        .Replace(":", string.Empty)
                        .Replace(";", string.Empty)
                        .Replace("'", string.Empty)
                        .Replace("!", string.Empty)
                        .Replace("-", string.Empty)
                        .Replace("·", string.Empty)
                        .Replace("?", string.Empty)
                        .Replace("½", "Half")
                ;
            int number = -1;
            for (int index = 0; index < littleClear.Length; index++)
            {
                var letter = littleClear[index];
                if (char.IsDigit(letter))
                {
                    if (number == -1 && index == 0)
                    {
                        number = index;
                    }
                    else if (number > -1 && index > 0)
                    {
                        number = index;
                    }
                }
                else if (number > -1)
                {
                    break;
                }
            }

            if (number > -1)
            {
                var numberPart = littleClear.Substring(0, number + 1);
                var humanInteger = HumanFriendlyInteger.IntegerToWritten(int.Parse(numberPart));
                var rest = littleClear.Substring(number + 1, littleClear.Length - (number + 1));
                return humanInteger + rest;
            }

            return littleClear;
        }


        public static string MakeNice(this string value)
        {
            return HttpUtility.HtmlEncode(value);
        }
    }

    public static class HumanFriendlyInteger
    {
        static string[] ones = new string[] { "", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine" };
        static string[] teens = new string[] { "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
        static string[] tens = new string[] { "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };
        static string[] thousandsGroups = { "", "Thousand", "Million", "Billion" };

        private static string FriendlyInteger(int n, string leftDigits, int thousands)
        {
            if (n == 0)
            {
                return leftDigits;
            }

            string friendlyInt = leftDigits;

            if (friendlyInt.Length > 0)
            {
                friendlyInt += "";
            }

            if (n < 10)
            {
                friendlyInt += ones[n];
            }
            else if (n < 20)
            {
                friendlyInt += teens[n - 10];
            }
            else if (n < 100)
            {
                friendlyInt += FriendlyInteger(n % 10, tens[n / 10 - 2], 0);
            }
            else if (n < 1000)
            {
                friendlyInt += FriendlyInteger(n % 100, (ones[n / 100] + "Hundred"), 0);
            }
            else
            {
                friendlyInt += FriendlyInteger(n % 1000, FriendlyInteger(n / 1000, "", thousands + 1), 0);
            }

            return friendlyInt + thousandsGroups[thousands];
        }

        public static string IntegerToWritten(int n)
        {
            if (n == 0)
            {
                return "Zero";
            }
            else if (n < 0)
            {
                return "Negative" + IntegerToWritten(-n);
            }

            return FriendlyInteger(n, "", 0);
        }
    }

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

                var tMovies = movies.Select(movie => new { dbName = movie.Title.ClearShit()+"Movie", Title= movie.Title.MakeNice(), ReleaseDate = movie.ReleaseDate.ToString("o"), movie.ImageUrl, Actors = movie.Actors.Select(a => new ActorBear {dbName = a.Name.ClearShit()+"Person", Name = a.Name.MakeNice(), ImageUrl = a.ImageUrl}) }).ToArray();

                var actorCommands = tMovies.SelectMany(m => m.Actors).Distinct(new DistictActor()).Select(actor => $"CREATE ({actor.dbName}:Person {{name:'{actor.Name}', imageUrl:'{actor.ImageUrl}'}})");
                var movieCommands = tMovies.Select(movie => $"CREATE ({movie.dbName}:Movie {{title:'{movie.Title}', releaseDate:'{movie.ReleaseDate}',imageUrl:'{movie.ImageUrl}'}})");
                var actedinCommands = tMovies.SelectMany(movie => movie.Actors, (movie, actor) => $@"MATCH ( {actor.dbName}:Person {{ name: '{actor.Name}'}})
                                                                                                     MATCH ( {movie.dbName}:Movie {{ title: '{movie.Title}'}})
                                                                                                     CREATE ({actor.dbName}) -[:ACTED_IN]-> ({movie.dbName})"
                                                                                                    
                                                                                                    ).ToList();

                foreach (var cmd in actorCommands)
                {
                    session.Run(cmd);
                }

                foreach (var cmd in movieCommands)
                {
                    session.Run(cmd);
                }
                foreach (var cmd in actedinCommands)
                {
                    session.Run(cmd);
                }

          
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
                return CreateActorConnection(readOnlyList);
            }
        }

        private ActorConnection CreateActorConnection(IEnumerable<INode> nodes)
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
                Movie = CreateMovieConnection(nodes.Except(new List<INode> { node }))
            };
        }

        private MovieConnection CreateMovieConnection(IEnumerable<INode> nodes)
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
                Actor = CreateActorConnection(nodes.Except(new List<INode> { node }))
            };
        }
    }

    public class ActorBear
    {
        public string dbName { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
    }

    public class DistictActor : IEqualityComparer<ActorBear>
    {
        public bool Equals(ActorBear x, ActorBear y)
        {
            return x.dbName.Equals(y.dbName);
        }

        public int GetHashCode(ActorBear obj)
        {
            return obj.dbName.GetHashCode();
        }
    }
}