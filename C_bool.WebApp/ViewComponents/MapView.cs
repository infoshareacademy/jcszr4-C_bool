using System.Collections.Generic;
using System.Threading.Tasks;
using C_bool.BLL.DAL.Entities;
using C_bool.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace C_bool.WebApp.ViewComponents
{
    public class MapView : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(IEnumerable<Place> placesList)
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

            return View("MapView", placesViewList);
        }
    }
}
