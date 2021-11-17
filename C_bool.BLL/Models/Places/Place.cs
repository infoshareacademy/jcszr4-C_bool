using System.Collections.Generic;
using Newtonsoft.Json;

namespace C_bool.BLL.Models.Places
{
    public class Place : IEntity
    {
        [JsonProperty(PropertyName = "place_id")]
        public string Id { get; set; }
        public Geometry Geometry { get; set; }
        public string Name { get; set; }
        public List<Photo> photos { get; set; }
        public string[] Types { get; set; }
        public double Rating { get; set; }
        [JsonProperty(PropertyName = "user_ratings_total")]
        public int UserRatingsTotal { get; set; }
        [JsonProperty(PropertyName = "vicinity")]
        public string Address { get; set; }
    }

    public class Photo
    {
        public int height { get; set; }
        public List<string> html_attributions { get; set; }
        public string photo_reference { get; set; }
        public int width { get; set; }
    }
}