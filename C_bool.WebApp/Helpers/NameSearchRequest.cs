using System.ComponentModel;

namespace C_bool.WebApp.Helpers
{
    public class NameSearchRequest
    {
        [DisplayName("Szukana fraza")] 
        public string SearchPhrase { get; set; }

    }
}
