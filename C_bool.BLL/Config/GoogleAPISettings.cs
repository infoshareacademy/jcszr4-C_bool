

namespace C_bool.BLL.Config
{
    public class GoogleAPISettings
    {
        public const string Position = "GoogleAPISettings";
        public string GoogleAPIKey { get; set; }
        public string CustomMapId { get; set; }
        public bool EnablePlacePhoto { get; set; }
        public bool EnableGeocoding { get; set; }
        public bool GetAllPages { get; set; }
    }
}
