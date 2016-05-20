using System.Collections.Generic;

namespace Web
{
    public class RootObject
    {
        public int page { get; set; }
        public List<Result> results { get; set; }
        public int total_pages { get; set; }
        public int total_results { get; set; }
    }
}
