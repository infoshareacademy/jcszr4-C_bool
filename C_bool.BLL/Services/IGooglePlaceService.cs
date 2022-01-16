using System.Collections.Generic;
using System.Threading.Tasks;
using C_bool.BLL.Models.GooglePlaces;

namespace C_bool.BLL.Services
{
    public interface IGooglePlaceService
    {
        public void CreateNewOrUpdateExisting(List<GooglePlace> googlePlaces);
        public List<GooglePlace> GetGooglePlacesForUser();
        public GooglePlace GetGooglePlaceById(string placeId);
    }
}