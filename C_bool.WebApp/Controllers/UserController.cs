using C_bool.BLL.DAL.Entities;
using C_bool.BLL.Repositories;
using C_bool.BLL.Services;
using C_bool.WebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace C_bool.WebApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IRepository<User> _userRepository;
        private readonly IUserService _userService;
        private readonly IReportService _reportService;
        private GeoLocation _geoLocation;

        public UserController(
            IRepository<User> userRepository,
            IUserService userService, IReportService reportService)
        {
            _userRepository = userRepository;
            _userService = userService;
            _reportService = reportService;
        }

        [Authorize(Roles = "Admin, Moderator, User")]
        [HttpPost]
        public IActionResult UpdateUserLocation([FromBody] GeoLocation postData)
        {
            var user = _userService.GetCurrentUser();
            if (postData.Latitude != 0)
            {
                _geoLocation = postData;
                
                if (user.Latitude == 0 && user.Longitude == 0)
                {
                    _reportService.UpdateUserReportEntry(user);
                }
                user.Latitude = _geoLocation.Latitude;
                user.Longitude = _geoLocation.Longitude;
                _userRepository.Update(user);
                return Json(new { success = true, responseText = "Odświeżono dane o lokalizacji" });
            }
            return Json(new { success = false, responseText = "Błąd pobierania lokalizacji. Położenie wskazuje że mieszkasz dokładnie na równiku, co jest raczej mało prawdopodobną opcją. Odśwież okno przeglądarki." });
        }
    }
}
