using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using C_bool.BLL.Models.Places;
using C_bool.BLL.Repositories;
using C_bool.WebApp.Interfaces;
using C_bool.WebApp.Models;
using C_bool.WebApp.Services;
using Microsoft.Extensions.Configuration;

namespace C_bool.WebApp.Controllers
{
    public class MapController : Controller
    {
        // GET: MapController
        private MapService _mapService;
        private PlacesRepository _placesRepository = new PlacesRepository();
        private GeoLocation _geoLocation;

        public static string apiKey;
        public static double Latitude;
        public static double Longitude;

        public IConfiguration configuration;

        public MapController(IConfiguration config)
        {
            _mapService = new MapService();
            configuration = config;
            apiKey = configuration.GetSection("AppSettings").GetSection("GoogleAPIKey").Value;

            //_placesRepository.AddFileDataToRepository();
            //_mapService.GetFromRepo(_placesRepository);

        }
        public ActionResult Index()
        {
            var model = _mapService.GetAll();
            return View(model);
        }

        public ActionResult NewRequest()
        {
            var model = new PlaceSearchRequest();
            model.Latitude = Latitude.ToString(CultureInfo.InvariantCulture);
            model.Longitude = Longitude.ToString(CultureInfo.InvariantCulture);
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

        // GET: MapController/Details/5
        public ActionResult Update()
        {
            _placesRepository.AddApiDataToRepository(Latitude.ToString(CultureInfo.InvariantCulture), Longitude.ToString(CultureInfo.InvariantCulture), 2000, apiKey, "restaurant", "PL", "en");
            _mapService.GetFromRepo(_placesRepository);
            var model = _mapService.GetAll();
            return View("~/Views/Map/Index.cshtml", model);
        }

        // GET: MapController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MapController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MapController/Create
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

        // POST: MapController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewRequest(PlaceSearchRequest request)
        {
            _placesRepository.AddApiDataToRepository(request.Latitude, request.Longitude, request.Radius, apiKey, request.SelectedType, "PL", "en");
            _mapService.GetFromRepo(_placesRepository);
            var model = _mapService.GetAll();
            return View("~/Views/Map/Index.cshtml", model);
        }

        // GET: MapController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MapController/Edit/5
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

        // GET: MapController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MapController/Delete/5
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
}
