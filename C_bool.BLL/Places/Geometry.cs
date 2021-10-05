using Newtonsoft.Json;

namespace C_bool.BLL.Places
{
    public class Geometry
    {
        [JsonProperty(PropertyName = "location")]
        public Location Location { get; set; }
    }
}