using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using C_bool.BLL.Models.Places;
using C_bool.BLL.Repositories;
using C_bool.WebApp.Config;
using C_bool.WebApp.Helpers;
using C_bool.WebApp.Models;
using C_bool.WebApp.Services;
using Microsoft.Extensions.Configuration;

namespace C_bool.WebApp.Controllers
{
    public class PlacesController : Controller
    {
        // GET: PlacesController
        private PlacesService _placesService;
        private IPlacesRepository _repository;
        private GeoLocation _geoLocation;

        public static double Latitude;
        public static double Longitude;

        public IConfiguration Configuration;

        public PlacesController(IConfiguration configuration, PlacesService placesService, IPlacesRepository repository)
        {
            _placesService = placesService;
            _repository = repository;
            Configuration = configuration;
        }

        public ActionResult Index()
        {
            var model = _placesService.GetAll();
            ViewBag.Message = $"Ilość miejsc w bazie: {model.Count}";
            ViewBag.Status = true;
            ViewBag.Latitude = Latitude;
            ViewBag.Longitude = Longitude;
            return View(model);
        }

        public ActionResult Favourities()
        {
            var model = _placesService.GetAll();
            ViewBag.Message = $"Ilość miejsc w ulubionych: {model.Count}";
            ViewBag.Status = true;
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
                ViewBag.Latitude = Latitude;
                ViewBag.Longitude = Longitude;
            }

            return Json(postData);
        }

        // GET: PlacesController/Details/5
        public ActionResult Details(string id)
        {
            var model = _repository.SearchById(id);
            return View(model);
        }

        // GET: PlacesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PlacesController/Create
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

        public async Task<IActionResult> Edit(string id)
        {
            var model = _repository.SearchById(id);


            return View("Edit", model);
        }

        // POST: PlacesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, Place model, IFormFile file)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                model.Photo = ImageConverter.ConvertImage(file);
                _repository.Update(model);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<IActionResult> Delete(string id)
        {
            _repository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
