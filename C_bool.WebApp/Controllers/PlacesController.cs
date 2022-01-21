using System.Collections.Generic;
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
            var user = _userService.GetCurrentUser();
            ViewBag.Latitude = user.Latitude;
            ViewBag.Longitude = user.Longitude;

            if (range == 0) range = 100000;

            ViewBag.Range = range / 1000;

            ViewData["CurrentFilter"] = searchString;
            ViewData["OnlyFavs"] = searchOnlyFavs;
            ViewData["OnlyTask"] = searchOnlyWithTasks;
            ViewData["CurrentRange"] = range;
            ViewData["MapZoom"] = range;

            var places = _placesService.GetNearbyPlacesQueryable(user.Latitude, user.Longitude, range);

            //search queries, based on user input
            if (!string.IsNullOrEmpty(searchString))
            {
                places = places.Where(p => p.Name.Contains(searchString) || p.Address.Contains(searchString) || p.ShortDescription.Contains(searchString));
            }

            if (searchOnlyFavs)
            {
                if (user.FavPlaces != null)
                {
                    places = places.Where(p => _userService.GetFavPlaces().Contains(p));
                }
            }

            if (searchOnlyWithTasks)
            {
                places = places.Where(p => p.Tasks.Any());
            }

            //places = places.OrderByDescending(x => x.Tasks.Count);

            //var model = places.Select(x => _mapper.Map<PlaceViewModel>(x)).ToList();
            var model = _mapper.Map<List<PlaceViewModel>>(places);
            ViewBag.PlacesCount = places.Count();

            ViewBag.Message = new StatusMessage($"Znaleziono {model.ToList().Count} pasujących miejsc", StatusMessage.Status.INFO);
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddToFavs([FromBody] ReturnString request)
        {
            var place = _placesService.GetPlaceById(request.Id);
            if (_userService.AddFavPlace(place))
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
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
