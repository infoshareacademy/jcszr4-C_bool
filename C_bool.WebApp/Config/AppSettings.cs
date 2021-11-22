

namespace C_bool.WebApp.Config
{
    public class AppSettings
    {
        public const string Position = "AppSettings";
        public string GoogleAPIKey { get; set; }
        public bool EnablePlacePhoto { get; set; }
        public bool EnableGeocoding { get; set; }
        public bool GetAllPages { get; set; }

    }
}
