using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using C_bool.BLL.Models.Places;
using C_bool.BLL.Repositories;
using C_bool.WebApp.Config;
using C_bool.WebApp.Models;
using C_bool.WebApp.Services;
using Microsoft.Extensions.Configuration;

namespace C_bool.WebApp.Controllers
{
    public class PlacesController : Controller
    {
        // GET: PlacesController
        private MapService _mapService;
        private IPlacesRepository _repository;
        private GeoLocation _geoLocation;

        public static double Latitude;
        public static double Longitude;

        public IConfiguration Configuration;

        public PlacesController(IConfiguration configuration, MapService mapService, IPlacesRepository repository)
        {
            _mapService = mapService;
            _repository = repository;
            Configuration = configuration;
        }

        public ActionResult Index()
        {
            var model = _mapService.GetAll();
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
            }

            return Json(postData);
        }

        // GET: PlacesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
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

        // GET: PlacesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PlacesController/Edit/5
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

        // GET: ProductsController/Delete/5
        public ActionResult Delete(string id)
        {
            var model = _repository.SearchById(id);
            return View(model);
        }

        // POST: ProductsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id, Place model)
        {
            try
            {
                _repository.Delete(id);
                ViewBag.Message = $"Usunięto miejsce: {model.Name}";
                ViewBag.Status = true;

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    
}
}
