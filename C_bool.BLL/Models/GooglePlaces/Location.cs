using Newtonsoft.Json;

namespace C_bool.BLL.Models.GooglePlaces
{
    public class Location
    {
        [JsonProperty(PropertyName = "lat")]
        public double Latitude { get; set; }

        [JsonProperty(PropertyName = "lng")]
        public double Longitude { get; set; }
    }
}
