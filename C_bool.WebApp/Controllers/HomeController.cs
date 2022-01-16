using System;
using C_bool.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using C_bool.BLL.DAL.Context;
using C_bool.BLL.DAL.Entities;
using C_bool.BLL.Logic;
using C_bool.BLL.Repositories;
using C_bool.BLL.Services;
using C_bool.WebApp.Helpers;
using C_bool.WebApp.Models.Place;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace C_bool.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IMapper _mapper;
        private IConfiguration _configuration;

        private readonly ApplicationDbContext _context;
        private IRepository<Place> _placesRepository;
        private IRepository<User> _usersRepository;
        private IRepository<GameTask> _gameTasksRepository;

        private readonly IPlaceService _placesService;
        private readonly IUserService _userService;
        private readonly UserManager<User> _userManager;


        public HomeController(
            ILogger<HomeController> logger,
            IMapper mapper,
            IConfiguration configuration,
            ApplicationDbContext context,
            IRepository<Place> placesRepository,
            IRepository<User> usersRepository,
            IRepository<GameTask> gameTasksRepository,
            IPlaceService placesService,
            IUserService usersService,
            UserManager<User> userManager)
        {
            _logger = logger;
            _mapper = mapper;
            _configuration = configuration;
            _context = context;
            _placesRepository = placesRepository;
            _usersRepository = usersRepository;
            _gameTasksRepository = gameTasksRepository;
            _placesService = placesService;
            _usersService = usersService;
            _userManager = userManager;
        }

        [Authorize]
        public async Task<IActionResult> Index(string searchString, string searchType, double range)
        {
            var userId = int.Parse(_userManager.GetUserId(User));
            var user = _usersRepository.GetById(userId);
            ViewBag.Latitude = user.Latitude;
            ViewBag.Longitude = user.Longitude;

            if (range == 0) range = 5000;
            ViewBag.Range = range / 1000;

            ViewData["CurrentFilter"] = searchString;
            ViewData["CurrentType"] = searchType;
            ViewData["CurrentRange"] = range;
            ViewData["MapZoom"] = range;

            //list of UserViewModel with top 10 users (by points count)
            var usersCount = _usersRepository.GetAllQueryable().Count();
            ViewBag.UserRank = _usersService.OrderByPoints(true).GetRange(0,usersCount < 10 ? usersCount : 10 ).Select(x => _mapper.Map<UserViewModel>(x)).ToList();

            ViewBag.UserPoints = user.Points;
            ViewBag.UserRankPosition = _usersRepository.GetAllQueryable().Count(x => x.Points > user.Points) + 1;

            //list of all places - count, last added
            var places = _placesRepository.GetAllQueryable();
            ViewBag.AllPlacesCount = places.Count();
            ViewBag.LastAddedPlace = _mapper.Map<PlaceViewModel>(places.OrderBy(x=> x.CreatedOn).LastOrDefault());

            //list of all gametasks in places - count
            var gameTasks = _gameTasksRepository.GetAllQueryable();
            ViewBag.ActiveTasksCount = gameTasks.Count(x => x.IsActive);
            ViewBag.LastAddedGameTask = gameTasks.OrderBy(x => x.CreatedOn).LastOrDefault();

            //query only properties used to calculate range ang get place Id
            var placesToSearchFrom = await places.Select(place => new Place() { Id = place.Id, Latitude = place.Latitude, Longitude = place.Longitude }).ToListAsync();
            var nearbyPlacesIds = SearchNearbyPlaces.GetPlacesId(placesToSearchFrom, user.Latitude, user.Longitude, range);
            places = places.Where(s => nearbyPlacesIds.Contains(s.Id));

            //search queries, based on user input
            if (!string.IsNullOrEmpty(searchString))
            {
                switch (searchType)
                {
                    case "place":
                        places = places.Where(s => s.Name.Contains(searchString)
                                                   || s.Address.Contains(searchString)
                                                   || s.ShortDescription.Contains(searchString));
                        break;
                    case "task":
                        places = places.Where(s => s.Tasks.Any(task => task.Name.Contains(searchString)) 
                                                   || s.Tasks.Any(task => task.ShortDescription.Contains(searchString)));
                        break;
                    default:
                        places = places.Where(s => s.Name.Contains(searchString)
                                                   || s.Address.Contains(searchString)
                                                   || s.ShortDescription.Contains(searchString)
                                                   || s.Tasks.Any(task => task.Name.Contains(searchString)) 
                                                   || s.Tasks.Any(task => task.ShortDescription.Contains(searchString)));
                        break;
                }
            }


            var placesList = await places.Select(x => _mapper.Map<PlaceViewModel>(x)).ToListAsync();
            ViewBag.NearbyPlacesCount = places.Count();
            ViewBag.NearbyPlaces = placesList;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
