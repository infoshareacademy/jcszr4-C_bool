using Newtonsoft.Json;

namespace C_bool.BLL.Models.Places
{
    public class Place : IEntity
    {
        [JsonProperty(PropertyName = "place_id")]
        public string Id { get; set; }
        public Geometry Geometry { get; set; }
        public string Name { get; set; }
        public string[] Types { get; set; }
        public double Rating { get; set; }
        [JsonProperty(PropertyName = "user_ratings_total")]
        public int UserRatingsTotal { get; set; }
        [JsonProperty(PropertyName = "vicinity")]
        public string Address { get; set; }
    }
}