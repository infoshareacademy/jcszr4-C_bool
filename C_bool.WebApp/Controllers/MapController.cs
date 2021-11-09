using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using C_bool.BLL.Models.Places;
using C_bool.BLL.Repositories;
using C_bool.WebApp.Services;

namespace C_bool.WebApp.Controllers
{
    public class MapController : Controller
    {
        // GET: MapController
        private MapService _mapService;
        private PlacesRepository _placesRepository = new();

        public MapController()
        {
            _mapService = new MapService();
            _placesRepository.AddFileDataToRepository();
            _mapService.GetFromRepo(_placesRepository);

        }
        public ActionResult Index()
        {
            var model = _mapService.GetAll();
            return View(model);
        }

        // GET: MapController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult ShowOnMap(int id)
        {
            return View();
        }

        // GET: MapController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MapController/Create
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

        // GET: MapController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MapController/Edit/5
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

        // GET: MapController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MapController/Delete/5
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
    }
}
