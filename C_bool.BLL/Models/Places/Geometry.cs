using Newtonsoft.Json;

namespace C_bool.BLL.Models.Places
{
    public class Geometry
    {
        [JsonProperty(PropertyName = "location")]
        public Location Location { get; set; }
    }
}