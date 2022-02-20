using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using C_bool.BLL.Helpers;
using C_bool.BLL.Models.GooglePlaces;


namespace C_bool.BLL.Services
{
    public class GooglePlaceService : IGooglePlaceService
    {
        private static readonly Dictionary<int, List<GooglePlace>> TempPlaces = new();
        private readonly IUserService _userService;

        public GooglePlaceService(IUserService userService)
        {
            _userService = userService;
        }

        public void CreateNewOrUpdateExisting(List<GooglePlace> googlePlaces)
        {
            var userId = _userService.GetCurrentUser().Id;

            if (TempPlaces.ContainsKey(userId))
            {
                TempPlaces[userId] = googlePlaces;
            }
            else
            {
                TempPlaces.Add(userId, googlePlaces);
            }
        }

        public List<GooglePlace> GetGooglePlacesForUser()
        {

            var test = TempPlaces;

            var userId = _userService.GetCurrentUser().Id;

            return TempPlaces.Where(kv => kv.Key == userId).Select(kv => kv.Value).SingleOrDefault();
        }

        public GooglePlace GetGooglePlaceById(string placeId)
        {
            var googlePlaces = GetGooglePlacesForUser();
            return googlePlaces.SingleOrDefault(gp => gp.Id == placeId);
        }
    }
}
