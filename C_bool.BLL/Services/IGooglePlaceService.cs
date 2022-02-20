using System.Collections.Generic;
using System.Threading.Tasks;
using C_bool.BLL.Models.GooglePlaces;

namespace C_bool.BLL.Services
{
    public interface IGooglePlaceService
    {
        void CreateNewOrUpdateExisting(List<GooglePlace> googlePlaces);
        List<GooglePlace> GetGooglePlacesForUser();
        GooglePlace GetGooglePlaceById(string placeId);
    }
}