using Newtonsoft.Json;

namespace C_bool.BLL.Models.GooglePlaces
{
    public class Geometry
    {
        [JsonProperty(PropertyName = "location")]
        public Location Location { get; set; }
    }
}