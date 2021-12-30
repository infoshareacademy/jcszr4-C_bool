using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using C_bool.BLL.DAL.Entities;
using C_bool.BLL.Repositories;
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
        private IRepository<Place> _placesRepository;
        private GeoLocation _geoLocation;
        //TODO: gdzie to trzymać? User ale bez bazy?
        public static double Latitude;
        public static double Longitude;

        private IConfiguration _configuration;
        private readonly IMapper _mapper;

        public PlacesController(IConfiguration configuration, PlacesService placesService, IRepository<Place> repository, IMapper mapper)
        {
            _placesService = placesService;
            _placesRepository = repository;
            _configuration = configuration;
            _mapper = mapper;
        }

        public ActionResult Index()
        {
            var model = _placesRepository.GetAll();
            ViewBag.Message = $"Ilość miejsc w bazie: {model.ToList().Count}";
            ViewBag.Status = true;
            ViewBag.Latitude = Latitude;
            ViewBag.Longitude = Longitude;
            return View(model);
        }

        public ActionResult Favourities()
        {
            var model = _placesRepository.GetAll();
            ViewBag.Message = $"Ilość miejsc w ulubionych: {model.ToList().Count}";
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
        public ActionResult Details(int id)
        {
            var model = _placesRepository.GetById(id);
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

        public async Task<IActionResult> Edit(int id)
        {
            //var model = _placesRepository.GetById(id);
            var model = _mapper.Map<Place, PlaceEditModel>(_placesRepository.GetById(id));


            return View("Edit", model);
        }

        // POST: PlacesController/Edit/5
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

        public async Task<IActionResult> Delete(int id)
        {
            _placesRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
