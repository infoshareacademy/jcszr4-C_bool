using System.Collections.Generic;
using System.Linq;
using C_bool.BLL.DAL.Entities;
using C_bool.BLL.Logic;
using C_bool.BLL.Models.GooglePlaces;
using C_bool.BLL.Repositories;

namespace C_bool.BLL.Services
{
    public class PlacesService
    {
        private IRepository<Place> _placeRepository;

        public PlacesService(IRepository<Place> placeRepository)
        {
            _placeRepository = placeRepository;
        }

        public List<GooglePlace> TempGooglePlaces;


        public List<Place> GetNearbyPlaces(double latitude, double longitude, double radius)
        {
            var queryPlaces = _placeRepository.GetAllQueryable();
            return SearchNearbyPlaces.GetPlaces(queryPlaces.ToList(), latitude, longitude, radius);

        }
    }
}
