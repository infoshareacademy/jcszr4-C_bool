using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using C_bool.BLL.DAL.Context;
using C_bool.BLL.DAL.Entities;
using C_bool.BLL.Models.GooglePlaces;
using C_bool.BLL.Repositories;
using C_bool.BLL.Services;
using C_bool.WebApp.Config;
using C_bool.WebApp.Helpers;
using C_bool.WebApp.Models;
using C_bool.WebApp.Models.Place;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace C_bool.WebApp.Controllers
{
    public class NewPlaceController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        private readonly ApplicationDbContext _context;
        private IRepository<Place> _placesRepository;
        private IRepository<User> _usersRepository;
        private IRepository<GameTask> _gameTasksRepository;

        private PlacesService _placesService;
        private UsersService _usersService;
        private readonly UserManager<User> _userManager;

        private IHttpClientFactory _clientFactory;
        private GoogleAPISettings _googleApiSettings = new();

        public NewPlaceController(
            ILogger<HomeController> logger,
            IMapper mapper,
            IConfiguration configuration,
            ApplicationDbContext context,
            IRepository<Place> placesRepository,
            IRepository<User> usersRepository,
            IRepository<GameTask> gameTasksRepository,
            PlacesService placesService,
            UsersService userService,
            UserManager<User> userManager,
            IHttpClientFactory clientFactory
        )
        {
            _logger = logger;
            _mapper = mapper;
            _configuration = configuration;
            _configuration.GetSection(GoogleAPISettings.Position).Bind(_googleApiSettings);
            _context = context;
            _placesRepository = placesRepository;
            _usersRepository = usersRepository;
            _gameTasksRepository = gameTasksRepository;
            _placesService = placesService;
            _usersService = userService;
            _userManager = userManager;
            _clientFactory = clientFactory;
        }

        [Authorize]
        public ActionResult Index()
        {
            var model = _placesService.TempGooglePlaces;
            return View();
        }

        [Authorize]
        public ActionResult SearchNearby()
        {
            var userId = int.Parse(_userManager.GetUserId(User));
            var user = _usersRepository.GetById(userId);
            var model = new NearbySearchRequest
            {

                Latitude = user.Latitude.ToString(CultureInfo.InvariantCulture),
                Longitude = user.Longitude.ToString(CultureInfo.InvariantCulture),

            };
            return View(model);
        }

        [Authorize]
        public ActionResult SearchByName()
        {
            var model = new NameSearchRequest();

            return View(model);
        }


        [Authorize]
        public ActionResult Create()
        {
            //var model = _mapper.Map<Place, PlaceEditModel>(_placesRepository.GetById(id));
            return View();
        }

        [Authorize]
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

        [Authorize]
        [HttpPost]
        public async Task AddToFavs([FromBody] ReturnString request)
        {
            var userId = int.Parse(_userManager.GetUserId(User));
            var placesList = GooglePlaceHolder._tempPlaces.Where(place => userId.Equals(place.Key)).Select(x => x.Value).FirstOrDefault();
            if (placesList != null)
                foreach (var googlePlace in placesList)
                {
                    var place = _mapper.Map<GooglePlace, Place>(googlePlace);
                    var api = new GoogleApiAsync(_clientFactory, _googleApiSettings.GoogleAPIKey);
                    var photo = await api.DownloadImageAsync(googlePlace, "600");
                    place.Photo = photo;

                    _placesRepository.Add(place);
                    _usersService.AddFavPlace(_usersRepository.GetById(userId), place);
                }
        }

        [Authorize]
        [HttpPost]
        public async Task AddToRepository([FromBody] ReturnString request)
        {
            var userId = int.Parse(_userManager.GetUserId(User));
            var placesList = GooglePlaceHolder._tempPlaces.Where(place => userId.Equals(place.Key)).Select(x => x.Value).FirstOrDefault();
            if (placesList != null)
                foreach (var googlePlace in placesList)
                {
                    var place = _mapper.Map<GooglePlace, Place>(googlePlace);
                    var api = new GoogleApiAsync(_clientFactory, _googleApiSettings.GoogleAPIKey);
                    var photo = await api.DownloadImageAsync(googlePlace, "600");
                    place.Photo = photo;
                    _placesRepository.Add(place);
                }
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SearchNearby(NearbySearchRequest request)
        {
            var api = new GoogleApiAsync(_clientFactory, _googleApiSettings.GoogleAPIKey);
            var userId = int.Parse(_userManager.GetUserId(User));
            GooglePlaceHolder.CreateNewOrUpdateExisting(GooglePlaceHolder._tempPlaces, userId, await api.GetNearby(request.Latitude, request.Longitude, request.Radius, type: request.SelectedType, keyword: request.Keyword, region: "PL", language: "pl", loadAllPages: _googleApiSettings.GetAllPages));

            var model = GooglePlaceHolder._tempPlaces.Where(x => userId.Equals(x.Key)).Select(x => x.Value).FirstOrDefault();
            ViewBag.Message = api.Message;
            ViewBag.QueryStatus = api.QueryStatus;
            return View("~/Views/NewPlace/Index.cshtml", model);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SearchByNameAsync(NameSearchRequest request)
        {
            var api = new GoogleApiAsync(_clientFactory, _googleApiSettings.GoogleAPIKey);
            var userId = int.Parse(_userManager.GetUserId(User));
            GooglePlaceHolder.CreateNewOrUpdateExisting(GooglePlaceHolder._tempPlaces, userId, await api.GetBySearchQuery(query: request.SearchPhrase, language: "pl"));

            var model = GooglePlaceHolder._tempPlaces.Where(x => userId.Equals(x.Key)).Select(x => x.Value).FirstOrDefault();
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
