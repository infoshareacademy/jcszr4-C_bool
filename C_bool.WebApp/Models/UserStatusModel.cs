using Newtonsoft.Json;

namespace C_bool.WebApp.Models
{
    public class UserStatusModel
    {
        [JsonConverter(typeof(bool))]
        public bool newStatus { get; set; }
    }
}