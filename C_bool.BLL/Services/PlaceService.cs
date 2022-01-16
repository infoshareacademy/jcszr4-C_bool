using System;
using System.Collections.Generic;
using System.Linq;
using C_bool.BLL.DAL.Entities;
using C_bool.BLL.Logic;
using C_bool.BLL.Repositories;

namespace C_bool.BLL.Services
{
    public class PlaceService : IPlaceService
    {
        private readonly IRepository<Place> _placeRepository;


        public PlaceService(IRepository<Place> placeRepository)
        {
            _placeRepository = placeRepository;
        }

        public List<Place> GetNearbyPlaces(double latitude, double longitude, double radius)
        {
            var queryPlaces = _placeRepository.GetAllQueryable();
            return SearchNearbyPlaces.GetPlaces(queryPlaces.ToList(), latitude, longitude, radius);
        }

        public Place GetPlaceById(int placeId)
        {
            var places = _placeRepository.GetAllQueryable();
            return places.SingleOrDefault(e => e.Id == placeId);
        }

        public Place GetPlaceById(string placeId)
        {
            var places = _placeRepository.GetAllQueryable();
            return places.SingleOrDefault(e => e.GoogleId == placeId);
        }

        public void AddPlace(Place place)
        {
            place.CreatedOn = DateTime.UtcNow;
            _placeRepository.Add(place);
        }
    }
}
