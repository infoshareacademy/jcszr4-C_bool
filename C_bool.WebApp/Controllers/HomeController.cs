using System;
using C_bool.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using C_bool.BLL.DAL.Context;
using C_bool.BLL.DAL.Entities;
using C_bool.BLL.Logic;
using C_bool.BLL.Repositories;
using C_bool.WebApp.Helpers;
using C_bool.WebApp.Models.Place;
using C_bool.WebApp.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace C_bool.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly ILogger<HomeController> _logger;
        private PlacesService _placesService;
        private IRepository<Place> _placesRepository;
        private GeoLocation _geoLocation;
        //TODO: gdzie to trzymać? User ale bez bazy?
        public static double Latitude;
        public static double Longitude;

        private IConfiguration _configuration;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, IConfiguration configuration, PlacesService placesService, IRepository<Place> repository, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _placesService = placesService;
            _placesRepository = repository;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<IActionResult> IndexAsync(string searchString, string searchType, double range)
        {
            ViewBag.Latitude = Latitude;
            ViewBag.Longitude = Longitude;

            if (range == 0) range = 5000;
            ViewBag.Range = range / 1000;

            ViewData["CurrentFilter"] = searchString;
            ViewData["CurrentType"] = searchType;
            ViewData["CurrentRange"] = range;
            ViewData["MapZoom"] = range;

            var places = _placesRepository.GetAllQueryable();

            //query only properties used to calculate range ang get place Id
            var placesToSearchFrom = await places.Select(place => new Place() { Id = place.Id, Latitude = place.Latitude, Longitude = place.Longitude }).ToListAsync();
            var nearbyPlacesIds = SearchNearbyPlaces.GetPlacesId(placesToSearchFrom, Latitude, Longitude, range);
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

            var placesList = await places.AsNoTracking().ToListAsync();
            ViewBag.PlacesCount = places.Count();
            ViewBag.NearbyPlaces = placesList;

            return View();
        }

        [HttpPost]
        public void GetGeoLocation([FromBody] GeoLocation postData)
        {
            if (postData.Latitude != 0)
            {
                _geoLocation = postData;
                Latitude = _geoLocation.Latitude;
                Longitude = _geoLocation.Longitude;
                ViewBag.Latitude = Latitude;
                ViewBag.Longitude = Longitude;
            }
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
