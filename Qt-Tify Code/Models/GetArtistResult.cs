using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QTtify.Models
{
    public class GetArtistResult
    {
        public Rootobject root { get; set; }
    }
    public class Rootobject
    {
        public External_Urls external_urls { get; set; }
        public Followers followers { get; set; }
        public string[] genres { get; set; }
        public string href { get; set; }
        public string id { get; set; }
        public Image[] images { get; set; }
        public string name { get; set; }
        public int popularity { get; set; }
        public string type { get; set; }
        public string uri { get; set; }
    }

    public class External_Urls_Artist
    {
        public string spotify { get; set; }
    }

    public class Followers
    {
        public object href { get; set; }
        public int total { get; set; }
    }

    public class Image_Artist
    {
        public int height { get; set; }
        public string url { get; set; }
        public int width { get; set; }
    }
}
