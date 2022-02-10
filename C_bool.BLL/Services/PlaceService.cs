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

        public IQueryable<Place> GetNearbyPlacesQueryable(double latitude, double longitude, double radius)
        {
            var queryPlaces = _placeRepository.GetAllQueryable();
            var placesToSearchFrom = queryPlaces.Select(place => new Place() { Id = place.Id, Latitude = place.Latitude, Longitude = place.Longitude }).ToList();
            var nearbyPlacesIds = SearchNearbyPlaces.GetPlacesId(placesToSearchFrom, latitude, longitude, radius);
            return queryPlaces.Where(s => nearbyPlacesIds.Contains(s.Id));
        }

        public Place GetPlaceById(int placeId)
        {
            var places = _placeRepository.GetAllQueryable();
            return places.SingleOrDefault(e => e.Id == placeId);
        }

        public Place GetPlaceById(string placeId)
        {
            var id = int.Parse(placeId);
            return _placeRepository.GetAllQueryable().SingleOrDefault(x => id.Equals(x.Id));
        }

        public void AddPlace(Place place)
        {
            if (place.GoogleId != null && _placeRepository.GetAllQueryable().Any(x => place.GoogleId.Equals(x.GoogleId))) return;
            place.IsActive = true;
            place.CreatedOn = DateTime.UtcNow;
            _placeRepository.Add(place);
        }
    }
}
