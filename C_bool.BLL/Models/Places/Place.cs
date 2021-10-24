using Newtonsoft.Json;

namespace C_bool.BLL.Models.Places
{
    public class Place
    {
        [JsonProperty(PropertyName = "place_id")]
        public string PlaceId { get; set; }

        [JsonProperty(PropertyName = "geometry")]
        public Geometry Geometry { get; set; }
        
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        
        [JsonProperty(PropertyName = "types")]
        public string[] Types { get; set; }
        
        [JsonProperty(PropertyName = "rating")]
        public double Rating { get; set; }
        
        [JsonProperty(PropertyName = "user_ratings_total")]
        public int UserRatingsTotal { get; set; }

        [JsonProperty(PropertyName = "vicinity")]
        public string Address { get; set; }
    }
}