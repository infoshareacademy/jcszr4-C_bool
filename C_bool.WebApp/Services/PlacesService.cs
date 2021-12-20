using System.Collections.Generic;
using C_bool.BLL.DAL.Entities;
using C_bool.BLL.Models.GooglePlaces;

namespace C_bool.WebApp.Services
{
    public class PlacesService
    {
        public List<GooglePlace> TempGooglePlaces;

        public Place MapGooglePlaceToPlace(GooglePlace googlePlace)
        {
            var place = new Place
            {
                GoogleId = googlePlace.Id,
                Name = googlePlace.Name,
                Latitude = googlePlace.Geometry.Location.Latitude,
                Longitude = googlePlace.Geometry.Location.Longitude,
                Types = googlePlace.Types.ToArray(),
                Rating = googlePlace.Rating,
                UserRatingsTotal = googlePlace.UserRatingsTotal,
                Address = googlePlace.Address
            };
            return place;
        }
    }
}
