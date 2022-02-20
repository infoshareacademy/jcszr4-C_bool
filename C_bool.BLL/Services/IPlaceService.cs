using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using C_bool.BLL.DAL.Entities;

namespace C_bool.BLL.Services
{
    public interface IPlaceService
    {
        IQueryable<Place> GetAllQueryable();
        Place GetById(int placeId);
        Place GetById(string placeId);
        List<Place> GetNearbyPlaces(double latitude, double longitude, double radius);
        IQueryable<Place> GetNearbyPlacesQueryable(double latitude, double longitude, double radius);
        void Add(Place place);
        void Update(Place place);
        void Delete(int placeId);
    }
}