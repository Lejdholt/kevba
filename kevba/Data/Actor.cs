namespace Data
{
    public class Actor
    {
        public string Name { get;  }
        public string ImageUrl { get;  }

        public Actor(string name, string imageUrl)
        {
            Name = name;
            ImageUrl = imageUrl;
        }
    }
}