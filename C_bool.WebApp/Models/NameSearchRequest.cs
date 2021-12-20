using System.ComponentModel;

namespace C_bool.WebApp.Models
{
    public class NameSearchRequest
    {
        [DisplayName("Szukana fraza")] 
        public string SearchPhrase { get; set; }

    }
}
