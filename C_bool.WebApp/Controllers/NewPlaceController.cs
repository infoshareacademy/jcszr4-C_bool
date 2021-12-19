using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using C_bool.BLL.DAL.Entities;
using C_bool.BLL.Repositories;
using C_bool.WebApp.Config;
using C_bool.WebApp.Helpers;
using C_bool.WebApp.Models;
using C_bool.WebApp.Services;
using Microsoft.Extensions.Configuration;

namespace C_bool.WebApp.Controllers
{
    public class NewPlaceController : Controller
    {
        private PlacesService _placesService;
        private GeoLocation _geoLocation;
        private IHttpClientFactory _clientFactory;
        private AppSettings _appSettings = new();

        private IRepository<Place> _repository;

        public static double Latitude;
        public static double Longitude;

        public IConfiguration Configuration;

        public NewPlaceController(IConfiguration configuration, IRepository<Place> repository, PlacesService placesService, IHttpClientFactory clientFactory)
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
            var model = _placesService.TempGooglePlaces;
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
        public ActionResult Create(Place model, IFormFile file)
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
                model.Photo = (ImageConverter.ConvertImage(file));
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
        public async Task AddToFavs([FromBody] ReturnString request)
        {
            foreach (var googlePlace in _placesService.TempGooglePlaces.Where(place => place.Id.Equals(request.Id)))
            {
                var place = _placesService.MapGooglePlaceToPlace(googlePlace);
                var api = new GoogleApiAsync(_clientFactory, _appSettings.GoogleAPIKey);
                var photo = await api.DownloadImageAsync(googlePlace, "600");
                place.Photo = photo;
                _repository.Add(place);
            }
            //return View();

            return;
        }

        [HttpPost]
        public async Task AddToRepository([FromBody] ReturnString request)
        {
            foreach (var googlePlace in _placesService.TempGooglePlaces.Where(place => place.Id.Equals(request.Id)))
            {
                var place = _placesService.MapGooglePlaceToPlace(googlePlace);
                var api = new GoogleApiAsync(_clientFactory, _appSettings.GoogleAPIKey);
                var photo = await api.DownloadImageAsync(googlePlace, "600");
                place.Photo = photo;
                _repository.Add(place);
            }
            //return View();

            return;
        }

        // POST: PlacesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SearchNearby(NearbySearchRequest request)
        {
            var api = new GoogleApiAsync(_clientFactory, _appSettings.GoogleAPIKey);

            _placesService.TempGooglePlaces = await api.GetNearby(request.Latitude, request.Longitude, request.Radius, type: request.SelectedType, keyword: request.Keyword, region: "PL", language: "pl", loadAllPages: _appSettings.GetAllPages);
            var model = _placesService.TempGooglePlaces;
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

            _placesService.TempGooglePlaces = await api.GetBySearchQuery(query: request.SearchPhrase, language: "pl");
            var model = _placesService.TempGooglePlaces;
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
