using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using C_bool.BLL.Models.Places;
using C_bool.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace C_bool.WebApp.ViewComponents
{
    public class MapView : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Place> placesList = Program.MainPlacesRepository.Repository;
            List<PlaceViewModel> placesViewList = JsonConvert.DeserializeObject<List<PlaceViewModel>>(JsonConvert.SerializeObject(placesList));
            return View("MapView", placesViewList);
        }
    }


}
