using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using C_bool.BLL.Logic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace C_bool.WebApp.Helpers
{
    public class NearbySearchRequest
    {
        [DisplayName("Szerokość geograficzna")]
        public string Latitude { get; set; }
        [DisplayName("Długość geograficzna")]
        public string Longitude { get; set; }
        [DisplayName("Promień")]
        [Range(100, 50000, ErrorMessage = "Możesz szukać w promieniu od 100 metrów do 50 kilometrów")]
        public int Radius { get; set; } = 5000;
        public List<string> Type { get; set; }

        [DisplayName("Słowa kluczowe")]
        public string Keyword { get; set; }
        public string SelectedType { get; set; } = "";

        public List<SelectListItem> PlaceCategories { get; set; } = SelectListItems.GooglePlaceCategories;

    }
}
