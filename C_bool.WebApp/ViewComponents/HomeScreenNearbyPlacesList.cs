using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using C_bool.BLL.DAL.Entities;
using C_bool.WebApp.Models;
using C_bool.WebApp.Models.Place;
using Microsoft.AspNetCore.Mvc;

namespace C_bool.WebApp.ViewComponents
{
    public class HomeScreenNearbyPlacesList : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(List<PlaceViewModel> placesList)
        {
            return View("HomeScreenNearbyPlacesList", placesList.Take(5).ToList());
        }
    }
}
