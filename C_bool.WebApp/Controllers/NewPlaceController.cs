using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using C_bool.BLL.DAL.Entities;
using C_bool.BLL.Models.GooglePlaces;
using C_bool.BLL.Repositories;
using C_bool.BLL.Services;
using C_bool.WebApp.Config;
using C_bool.WebApp.Helpers;
using C_bool.WebApp.Models;
using C_bool.WebApp.Models.Place;
using C_bool.WebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;

namespace C_bool.WebApp.Controllers
{
    public class NewPlaceController : Controller
    {
        private PlacesService _placesService;
        private UsersService _usersService;
        private GeoLocation _geoLocation;
        private IHttpClientFactory _clientFactory;
        private GoogleAPISettings _googleApiSettings = new();

        private IRepository<Place> _placesRepository;

        public static double Latitude;
        public static double Longitude;

        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public NewPlaceController(IConfiguration configuration, IRepository<Place> repository, PlacesService placesService, UsersService userService, IHttpClientFactory clientFactory, IMapper mapper)
        {
            _placesService = placesService;
            _usersService = userService;
            _placesRepository = repository;
            _configuration = configuration;
            _configuration.GetSection(GoogleAPISettings.Position).Bind(_googleApiSettings);
            _clientFactory = clientFactory;
            _mapper = mapper;
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
            //var model = _mapper.Map<Place, PlaceEditModel>(_placesRepository.GetById(id));
            return View();
        }

        // POST: NewPlaceController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PlaceEditModel model, IFormFile file)
        {
            var placeModel = _mapper.Map<PlaceEditModel, Place>(model);
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.Message = "Nie udało się utworzyć miejsca";
                    ViewBag.Status = false;
                    return View(model);
                }

                placeModel.IsUserCreated = true;
                placeModel.Photo = (ImageConverter.ConvertImage(file));
                _placesRepository.Add(placeModel);
                ViewBag.Message = $"Dodano nowe miejsce: {placeModel.Name}";
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
            foreach (var googlePlace in GooglePlaceHolder._tempPlaces.Where(place => place.Id.Equals(request.Id)))
            {
                var place = _mapper.Map<GooglePlace, Place>(googlePlace);
                var api = new GoogleApiAsync(_clientFactory, _googleApiSettings.GoogleAPIKey);
                var photo = await api.DownloadImageAsync(googlePlace, "600");
                place.Photo = photo;

                //var user = _usersService.;

                //_usersService.AddFavPlace();
            }
            //return View();

            return;
        }

        [HttpPost]
        public async Task AddToRepository([FromBody] ReturnString request)
        {
            foreach (var googlePlace in GooglePlaceHolder._tempPlaces.Where(place => place.Id.Equals(request.Id)))
            {
                var place = _mapper.Map<GooglePlace, Place>(googlePlace);
                var api = new GoogleApiAsync(_clientFactory, _googleApiSettings.GoogleAPIKey);
                var photo = await api.DownloadImageAsync(googlePlace, "600");
                place.Photo = photo;
                _placesRepository.Add(place);
            }
            //return View();

            return;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SearchNearby(NearbySearchRequest request)
        {
            var api = new GoogleApiAsync(_clientFactory, _googleApiSettings.GoogleAPIKey);

            GooglePlaceHolder._tempPlaces = await api.GetNearby(request.Latitude, request.Longitude, request.Radius, type: request.SelectedType, keyword: request.Keyword, region: "PL", language: "pl", loadAllPages: _googleApiSettings.GetAllPages);
            var model = GooglePlaceHolder._tempPlaces;
            ViewBag.Message = api.Message;
            ViewBag.QueryStatus = api.QueryStatus;
            return View("~/Views/NewPlace/Index.cshtml", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SearchByNameAsync(NameSearchRequest request)
        {
            var api = new GoogleApiAsync(_clientFactory, _googleApiSettings.GoogleAPIKey);

            GooglePlaceHolder._tempPlaces = await api.GetBySearchQuery(query: request.SearchPhrase, language: "pl");
            var model = GooglePlaceHolder._tempPlaces;
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
