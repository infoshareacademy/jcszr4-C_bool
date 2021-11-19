using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using C_bool.BLL.Models.Places;
using C_bool.BLL.Repositories;
using C_bool.WebApp.Models;
using C_bool.WebApp.Services;
using Microsoft.Extensions.Configuration;

namespace C_bool.WebApp.Controllers
{
    public class NewPlaceController : Controller
    {
        private MapService _mapService;

        private PlacesRepository _tempRepository = new PlacesRepository();

        private GeoLocation _geoLocation;

        public static string apiKey;
        public static double Latitude;
        public static double Longitude;

        public IConfiguration configuration;

        public NewPlaceController(IConfiguration config)
        {
            _mapService = new MapService();
            configuration = config;
            apiKey = configuration.GetSection("AppSettings").GetSection("GoogleAPIKey").Value;

            //_placesRepository.AddFileDataToRepository();
            //_mapService.GetFromRepo(_placesRepository);

        }

        // GET: NewPlaceController
        public ActionResult Index()
        {
            var model = Program.TempPlaces;
            return View();
        }

        public ActionResult NewRequest()
        {
            var model = new PlaceSearchRequest
            {
                Latitude = Latitude.ToString(CultureInfo.InvariantCulture),
                Longitude = Longitude.ToString(CultureInfo.InvariantCulture)
            };

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

        // GET: NewPlaceController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: NewPlaceController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NewPlaceController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: NewPlaceController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add([FromBody]ReturnString request)
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
        public ActionResult NewRequest(PlaceSearchRequest request)
        {
            Program.TempPlaces = _tempRepository.ApiDataToPlacesList(request.Latitude, request.Longitude, request.Radius, apiKey, request.SelectedType, "PL", "en");
            var model = Program.TempPlaces;
            return View("~/Views/NewPlace/Index.cshtml", model);
        }

        // POST: NewPlaceController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: NewPlaceController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: NewPlaceController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }

    public class ReturnString
    {
        public string Id { get; set; }
    }
}
