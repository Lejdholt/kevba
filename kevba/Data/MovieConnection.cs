namespace Data
{
    public class MovieConnection
    {
        public string MovieTitle { get; set; }
        public string ImageUrl { get; set; }
        public ActorConnection Actor { get; set; }
    }
}