using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using C_bool.BLL.DAL.Entities;

namespace C_bool.BLL.Services
{
    public interface IPlaceService
    {
        public List<Place> GetNearbyPlaces(double latitude, double longitude, double radius);
        public IQueryable<Place> GetNearbyPlacesQueryable(double latitude, double longitude, double radius);
        public Place GetPlaceById(int placeId);
        public Place GetPlaceById(string placeId);
        public void AddPlace(Place place);
    }
}