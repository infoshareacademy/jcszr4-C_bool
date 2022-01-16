using System.Globalization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using C_bool.BLL.DAL.Context;
using C_bool.BLL.DAL.Entities;
using C_bool.BLL.Logic;
using C_bool.BLL.Repositories;
using C_bool.BLL.Services;
using C_bool.WebApp.Config;
using C_bool.WebApp.Helpers;
using C_bool.WebApp.Models;
using C_bool.WebApp.Models.Place;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace C_bool.WebApp.Controllers
{
    public class PlacesController : Controller
    {
        private readonly ILogger<PlacesController> _logger;
        private IUserService _userService;
        private IPlaceService _placesService;
        private IRepository<Place> _placesRepository;


        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        private readonly ApplicationDbContext _context;
        private IRepository<User> _usersRepository;
        private IRepository<GameTask> _gameTasksRepository;

        private readonly UserManager<User> _userManager;

        private IHttpClientFactory _clientFactory;

        public PlacesController(
            ILogger<PlacesController> logger,
            IMapper mapper,
            IConfiguration configuration,
            ApplicationDbContext context,
            IRepository<Place> placesRepository,
            IRepository<User> usersRepository,
            IRepository<GameTask> gameTasksRepository,
            IPlaceService placesService,
            IUserService userService,
            UserManager<User> userManager,
            IHttpClientFactory clientFactory
            )
        {
            _logger = logger;
            _mapper = mapper;
            _configuration = configuration;
            _context = context;
            _placesRepository = placesRepository;
            _usersRepository = usersRepository;
            _gameTasksRepository = gameTasksRepository;
            _placesService = placesService;
            _userService = userService;
            _userManager = userManager;
            _clientFactory = clientFactory;
        }

        [Authorize]
        public async Task<IActionResult> Index(string searchString, bool searchOnlyFavs, bool searchOnlyWithTasks, double range)
        {
            var userId = int.Parse(_userManager.GetUserId(User));
            var user = _usersRepository.GetById(userId);
            ViewBag.Latitude = user.Latitude;
            ViewBag.Longitude = user.Longitude;

            if (range == 0) range = 100000;

            ViewBag.Range = range / 1000;

            ViewData["CurrentFilter"] = searchString;
            ViewData["OnlyFavs"] = searchOnlyFavs;
            ViewData["OnlyTask"] = searchOnlyWithTasks;
            ViewData["CurrentRange"] = range;
            ViewData["MapZoom"] = range;

            //var user = _userManager.GetUserId(User);

            var places = _placesRepository.GetAllQueryable();

            //query only properties used to calculate range ang get place Id
            var placesToSearchFrom = await places.Select(place => new Place() { Id = place.Id, Latitude = place.Latitude, Longitude = place.Longitude }).ToListAsync();
            var nearbyPlacesIds = SearchNearbyPlaces.GetPlacesId(placesToSearchFrom, user.Latitude, user.Longitude, range);
            places = places.Where(s => nearbyPlacesIds.Contains(s.Id));

            //search queries, based on user input
            if (!string.IsNullOrEmpty(searchString))
            {
                places = places.Where(s => s.Name.Contains(searchString)
                                           || s.Address.Contains(searchString)
                                           || s.ShortDescription.Contains(searchString));

            }

            if (searchOnlyFavs)
            {
                if (user.FavPlaces != null)
                {
                    var favPlacesIds = user.FavPlaces.Select(x => x.PlaceId).ToArray();
                    places = places.Where(s => favPlacesIds.Contains(s.Id));
                }
            }

            if (searchOnlyWithTasks)
            {

                places = places.Where(x => x.Tasks.Any());

            }

            var model = await places.Select(x => _mapper.Map<PlaceViewModel>(x)).ToListAsync();
            ViewBag.PlacesCount = places.Count();

            ViewBag.Message = $"Znaleziono {model.ToList().Count} pasujących miejsc";
            ViewBag.Status = true;

            return View(model);
        }

        public ActionResult Favourities()
        {
            var model = _placesRepository.GetAll();
            ViewBag.Message = $"Ilość miejsc w ulubionych: {model.ToList().Count}";
            ViewBag.Status = true;
            return View(model);
        }


        [Authorize]
        public ActionResult Details(int id)
        {
            var model = _placesRepository.GetById(id);
            ViewBag.HasAnyActiveTasks = model.Tasks != null && model.Tasks.Any(x => x.IsActive);
            return View(model);
        }

        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize]
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

        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            //var model = _placesRepository.GetById(id);
            var model = _mapper.Map<Place, PlaceEditModel>(_placesRepository.GetById(id));


            return View("Edit", model);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, PlaceEditModel model, IFormFile file)
        {
            var place = _placesRepository.GetById(id);
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                place = _mapper.Map<PlaceEditModel, Place>(model, place);
                if (file != null) { place.Photo = ImageConverter.ConvertImage(file); }
                _placesRepository.Update(place);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            _placesRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
