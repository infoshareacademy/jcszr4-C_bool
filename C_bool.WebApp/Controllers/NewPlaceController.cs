using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using C_bool.BLL.Logic;
using C_bool.BLL.Models.Places;
using C_bool.BLL.Repositories;
using C_bool.WebApp.Config;
using C_bool.WebApp.Models;
using C_bool.WebApp.Services;
using Microsoft.Extensions.Configuration;

namespace C_bool.WebApp.Controllers
{
    public class NewPlaceController : Controller
    {
        private MapService _mapService;

        private GeoLocation _geoLocation;

        private AppSettings _appSettings = new AppSettings();

        public static double Latitude;
        public static double Longitude;

        public IConfiguration Configuration;

        public NewPlaceController(IConfiguration configuration)
        {
            _mapService = new MapService();
            Configuration = configuration;
            Configuration.GetSection(AppSettings.Position).Bind(_appSettings);
        }

        // GET: NewPlaceController
        public ActionResult Index()
        {
            var model = Program.TempPlaces;
            return View();
        }

        public ActionResult SearchNearby()
        {
            var model = new NearbySearchRequest
            {
                Latitude = Latitude.ToString(CultureInfo.InvariantCulture),
                Longitude = Longitude.ToString(CultureInfo.InvariantCulture)
            };

            return View(model);
        }

        public ActionResult SearchByName()
        {
            var model = new NameSearchRequest();

            return View(model);
        }

        [HttpPost]
        public JsonResult GetGeoLocation([FromBody] GeoLocation postData)
        {
            if (postData.Latitude != 0)
            {
                _geoLocation = postData;
                Latitude = _geoLocation.Latitude;
                Longitude = _geoLocation.Longitude;
            }

            return Json(postData);
        }

        // GET: NewPlaceController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NewPlaceController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Place model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                //model.Id = Guid.NewGuid().ToString().Replace("-", "");
                model.IsUserCreated = true;
                Program.MainPlacesRepository.Add(model);
                return RedirectToAction("Index","Places");
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult AddToFavs([FromBody]ReturnString request)
        {
            foreach (var place in Program.TempPlaces.Where(place => place.Id.Equals(request.Id)))
            {
                Program.MainPlacesRepository.Add(place);
            }
            return View();
        }

        // POST: PlacesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SearchNearby(NearbySearchRequest request)
        {
            Program.TempPlaces = GoogleAPI.ApiGetNearbyPlaces(request.Latitude, request.Longitude, request.Radius, _appSettings.GoogleAPIKey, request.SelectedType,request.Keyword, "PL", "pl", _appSettings.GetAllPages);
            var model = Program.TempPlaces;
            return View("~/Views/NewPlace/Index.cshtml", model);
        }

        // POST: PlacesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SearchByName(NameSearchRequest request)
        {
            Program.TempPlaces = GoogleAPI.ApiSearchPlaces(_appSettings.GoogleAPIKey,request.SearchPhrase, "pl");
            var model = Program.TempPlaces;
            return View("~/Views/NewPlace/Index.cshtml", model);
        }

    }

    public class ReturnString
    {
        public string Id { get; set; }
    }
}
