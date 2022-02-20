using System;
using System.Collections.Generic;
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
using C_bool.WebApp.Models.User;
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

        private readonly IPlaceService _placesService;
        private readonly IUserService _userService;
        private readonly IGameTaskService _gameTaskService;
        private readonly UserManager<User> _userManager;


        public HomeController(
            ILogger<HomeController> logger,
            IMapper mapper,
            IConfiguration configuration,
            ApplicationDbContext context,
            IPlaceService placesService,
            IUserService usersService,
            UserManager<User> userManager, 
            IGameTaskService gameTaskService)
        {
            _logger = logger;
            _mapper = mapper;
            _configuration = configuration;
            _context = context;
            _placesService = placesService;
            _userService = usersService;
            _userManager = userManager;
            _gameTaskService = gameTaskService;
        }

        [Authorize]
        public async Task<IActionResult> Index(string searchString, bool searchOnlyWithTasks, double range)
        {
            var user = _userService.GetCurrentUser();
            ViewBag.Latitude = user.Latitude;
            ViewBag.Longitude = user.Longitude;

            if (range == 0) range = 5000;

            //list of UserViewModel with top 10 users (by points count)
            var usersCount = _userService.GetAllQueryable().Count();
            ViewBag.UserRank = _userService.OrderByPoints(true).GetRange(0,usersCount < 10 ? usersCount : 10 ).Select(x => _mapper.Map<UserViewModel>(x)).ToList();

            ViewBag.UserPoints = user.Points;
            ViewBag.UserRankPosition = _userService.GetAllQueryable().Count(x => x.Points > user.Points) + 1;

            //list of all places - count, last added
            var places = _placesService.GetAllQueryable();
            ViewBag.AllPlacesCount = places.Count();
            ViewBag.LastAddedPlace = _mapper.Map<PlaceViewModel>(places.OrderBy(x=> x.CreatedOn).LastOrDefault());

            //list of all gametasks in places - count
            var gameTasks = _gameTaskService.GetAllQueryable();
            //ViewBag.ActiveTasksCount = gameTasks.Count(x => x.IsActive);
            ViewBag.ActiveTasksCount = gameTasks.Count();
            ViewBag.LastAddedGameTask = gameTasks.OrderBy(x => x.CreatedOn).LastOrDefault();

            //query only properties used to calculate range ang get place Id
            var placesToSearchFrom = await places.Select(place => new Place() { Id = place.Id, Latitude = place.Latitude, Longitude = place.Longitude }).ToListAsync();
            var nearbyPlacesIds = SearchNearbyPlaces.GetPlacesId(placesToSearchFrom, user.Latitude, user.Longitude, range);
            if (nearbyPlacesIds.Count == 0)
            {
                range = 100000;
                nearbyPlacesIds = SearchNearbyPlaces.GetPlacesId(placesToSearchFrom, user.Latitude, user.Longitude, range);
            }

            //get places by range
            places = places.Where(s => nearbyPlacesIds.Contains(s.Id));

            //search queries, based on user input
            if (!string.IsNullOrEmpty(searchString))
            {
                places = places.Where(s => s.Name.Contains(searchString)
                                           || s.Address.Contains(searchString)
                                           || s.ShortDescription.Contains(searchString));
            }

            if (searchOnlyWithTasks)
            {
                places = places.Where(p => p.Tasks.Any());
            }

            places = places.Include(x => x.Tasks).OrderByDescending(x => x.Tasks.Count);

            var placesList = _mapper.Map<List<PlaceViewModel>>(places);

            ViewBag.Range = range / 1000;

            ViewData["CurrentFilter"] = searchString;
            ViewData["OnlyTask"] = searchOnlyWithTasks;
            ViewData["CurrentRange"] = range;
            ViewData["MapZoom"] = range;

            ViewBag.NearbyPlacesCount = places.Count();
            ViewBag.NearbyPlaces = placesList;
            ViewBag.Message = new StatusMessage($"Znaleziono {placesList.ToList().Count} pasujących miejsc", StatusMessage.Status.INFO);

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
