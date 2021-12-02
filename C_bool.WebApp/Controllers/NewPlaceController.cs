using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
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
        private PlacesService _placesService;
        private IPlacesRepository _repository;
        private GeoLocation _geoLocation;
        private IHttpClientFactory _clientFactory;
        private AppSettings _appSettings = new();

        public static double Latitude;
        public static double Longitude;

        private static string _message;

        public IConfiguration Configuration;

        public NewPlaceController(IConfiguration configuration, PlacesService placesService, IPlacesRepository repository, IHttpClientFactory clientFactory)
        {
            _placesService = placesService;
            _repository = repository;
            Configuration = configuration;
            Configuration.GetSection(AppSettings.Position).Bind(_appSettings);
            _clientFactory = clientFactory;
        }

        // GET: NewPlaceController
        public ActionResult Index()
        {
            var model = _placesService.TempPlaces;
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
                    ViewBag.Message = "Nie udało się utworzyć miejsca";
                    ViewBag.Status = false;
                    return View(model);
                }

                model.IsUserCreated = true;
                _repository.Add(model);
                ViewBag.Message = $"Dodano nowe miejsce: {model.Name}";
                ViewBag.Status = true;
                return RedirectToAction("Index", "Places");
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult AddToFavs([FromBody] ReturnString request)
        {
            foreach (var place in _placesService.TempPlaces.Where(place => place.Id.Equals(request.Id)))
            {
                _repository.Add(place);
            }
            return View();
        }

        // POST: PlacesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SearchNearby(NearbySearchRequest request)
        {
            var api = new GoogleApiAsync(_clientFactory, _appSettings.GoogleAPIKey);

            _placesService.TempPlaces = await api.GetNearby(request.Latitude, request.Longitude, request.Radius, type: request.SelectedType, keyword: request.Keyword, region: "PL", language: "pl", loadAllPages: _appSettings.GetAllPages);
            var model = _placesService.TempPlaces;
            ViewBag.Message = api.Message;
            ViewBag.QueryStatus = api.QueryStatus;
            return View("~/Views/NewPlace/Index.cshtml", model);
        }

        // POST: PlacesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SearchByNameAsync(NameSearchRequest request)
        {
            var api = new GoogleApiAsync(_clientFactory, _appSettings.GoogleAPIKey);

            _placesService.TempPlaces = await api.GetBySearchQuery(query: request.SearchPhrase, language: "pl");
            var model = _placesService.TempPlaces;
            ViewBag.Message = api.Message;
            ViewBag.QueryStatus = api.QueryStatus;
            return View("~/Views/NewPlace/Index.cshtml", model);
        }

    }

    public class ReturnString
    {
        public string Id { get; set; }
    }
}
