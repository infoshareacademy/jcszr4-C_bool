using System.Collections.Generic;
using System.Linq;
using C_bool.BLL.DAL.Entities;
using C_bool.BLL.Logic;
using C_bool.BLL.Models.GooglePlaces;
using C_bool.BLL.Repositories;
using C_bool.WebApp.Models;

namespace C_bool.WebApp.Services
{
    public class PlacesService
    {
        public List<GooglePlace> TempGooglePlaces;

        public List<Place> GetNearbyPlaces(IRepository<Place> repository, double latitude, double longitude, double radius)
        {
            var queryPlaces = repository.GetAll();
            return SearchNearbyPlaces.GetPlaces(queryPlaces.ToList(), latitude, longitude, radius);

        }
    }
}
