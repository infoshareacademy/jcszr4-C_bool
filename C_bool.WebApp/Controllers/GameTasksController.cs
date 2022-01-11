using AutoMapper;
using C_bool.BLL.DAL.Entities;
using C_bool.BLL.Repositories;
using C_bool.WebApp.Models;
using C_bool.WebApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace C_bool.WebApp.Controllers
{
    public class GameTasksController : Controller
    {
        private GameTaskService _tasksService;
        private PlacesService _placesService;

        private IRepository<GameTask> _tasksRepository;
        private IRepository<Place> _placesRepository;

        private GeoLocation _geoLocation;
        //TODO: gdzie to trzymać? User ale bez bazy?
        public static double Latitude;
        public static double Longitude;

        private IConfiguration _configuration;
        private readonly IMapper _mapper;

        public GameTasksController(IConfiguration configuration, GameTaskService tasksService, PlacesService placesService, IRepository<GameTask> tasksRepository, IRepository<Place> placesRepository, IMapper mapper)
        {
            _tasksService = tasksService;
            _placesService = placesService;

            _tasksRepository = tasksRepository;
            _placesRepository = placesRepository;

            _configuration = configuration;
            _mapper = mapper;
        }

        // GET: GameTasksController
        public ActionResult Index()
        {
            return View();
        }

        // GET: GameTasksController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: GameTasksController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GameTasksController/Create
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

        // GET: GameTasksController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: GameTasksController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

        // GET: GameTasksController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: GameTasksController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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

        [HttpPost]
        public JsonResult GetGeoLocation([FromBody] GeoLocation postData)
        {
            if (postData.Latitude != 0)
            {
                _geoLocation = postData;
                Latitude = _geoLocation.Latitude;
                Longitude = _geoLocation.Longitude;
                ViewBag.Latitude = Latitude;
                ViewBag.Longitude = Longitude;
            }

            return Json(postData);
        }
    }
}
