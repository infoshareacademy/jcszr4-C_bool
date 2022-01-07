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

        public async Task<IActionResult> IndexAsync_test(string searchString, string limit)
        {
            //ViewBag.NearbyPlaces = await _placesService.GetNearbyPlaces(Latitude, Longitude, 10000);
            ViewBag.Latitude = Latitude;
            ViewBag.Longitude = Longitude;

            ViewData["CurrentFilter"] = searchString;

            var places = _placesRepository.GetAllQueryable();

            if (!String.IsNullOrEmpty(searchString))
            {
                places = places.Where(s => s.Name.Contains(searchString)
                                               || s.Address.Contains(searchString));
            }

            if (!String.IsNullOrEmpty(limit))
            {
                places = places.Where(s => s.Name.Contains(searchString)
                                           || s.Address.Contains(searchString));
            }


            var placesList = await places.AsNoTracking().ToListAsync();
            ViewBag.PlacesCount = places.Count();
            var nearbyPlaces = SearchNearbyPlaces.GetPlaces(placesList, Latitude, Longitude, 10000);
            ViewBag.NearbyPlaces = nearbyPlaces;

            return View();

        }

        public IActionResult Index()
        {
            ViewBag.PlacesCount = _placesRepository.GetAll().Count();
            ViewBag.NearbyPlaces = _placesService.GetNearbyPlaces(Latitude, Longitude, 10000);
            ViewBag.Latitude = Latitude;
            ViewBag.Longitude = Longitude;
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
