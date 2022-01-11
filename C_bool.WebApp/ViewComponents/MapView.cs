using System.Collections.Generic;
using System.Threading.Tasks;
using C_bool.BLL.DAL.Entities;
using C_bool.WebApp.Models;
using C_bool.WebApp.Models.Place;
using Microsoft.AspNetCore.Mvc;

namespace C_bool.WebApp.ViewComponents
{
    public class MapView : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(IEnumerable<Place> placesList, double range)
        {
            List<PlaceViewModel> placesViewList = new();
            foreach (var item in placesList)
            {
                var viewModel = new PlaceViewModel();
                viewModel.Id = item.Id;
                viewModel.Name = item.Name?.Replace("\"","");
                viewModel.Address = item.Address?.Replace("\"", "");
                viewModel.ShortDescription = item.ShortDescription?.Replace("\"", "");
                viewModel.Latitude = item.Latitude;
                viewModel.Longitude = item.Longitude;
                if (item.Tasks != null)
                {
                    viewModel.ActiveTaskCount = item.Tasks.Count;
                }
                placesViewList.Add(viewModel);
            }

            range = range;

            var zoom = range switch
            {
                < 1000 => 16,
                > 1000 and <= 5000 => 13,
                > 5000 and <= 10000 => 13,
                > 10000 and <= 18000 => 12,
                > 18000 and <= 30000 => 11,
                > 30000 and <= 100000 => 10,
                > 100000 and <= 200000 => 9,
                > 200000 and <= 400000 => 8,
                > 400000 and <= 800000 => 7,
                > 800000 and <= 1600000 => 6,
                > 1600000 => 5,
                _ => 15
            };

            ViewBag.MapZoom = zoom;

            return View("MapView", placesViewList);
        }
    }
}
