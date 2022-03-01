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
using C_bool.WebApp.Models.GameTask;
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
        private readonly IUserService _userService;
        private readonly IPlaceService _placesService;

        private readonly IMapper _mapper;

        public PlacesController(
            ILogger<PlacesController> logger,
            IMapper mapper,
            IPlaceService placesService,
            IUserService userService
        )
        {
            _logger = logger;
            _mapper = mapper;
            _placesService = placesService;
            _userService = userService;
        }

        [Authorize]
        public async Task<IActionResult> Index(string searchString, bool searchOnlyFavs, bool searchOnlyWithTasks, double range)
        {
            var user = _userService.GetCurrentUser();
            ViewBag.Latitude = user.Latitude;
            ViewBag.Longitude = user.Longitude;

            if (range == 0) range = 40000000;

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

            var favPlacesId = await places.Where(p => _userService.GetFavPlaces().Contains(p)).Select(x => x.Id).ToListAsync();

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

            var model = _mapper.Map<List<PlaceViewModel>>(places.AsNoTracking().Include(x => x.Tasks));
            await AssignViewModelProperties(model);

            model.OrderByDescending(x => x.DistanceFromUser);
            

            ViewBag.PlacesCount = places.Count();

            ViewBag.Message = new StatusMessage($"Znaleziono {model.ToList().Count} pasujących miejsc", StatusMessage.Status.INFO);
            return View(model);
        }

        private async Task AssignViewModelProperties(List<PlaceViewModel> model)
        {
            var user = _userService.GetCurrentUser();
            var favPlacesId = await _placesService.GetAllQueryable().Where(p => _userService.GetFavPlaces().Contains(p)).Select(x => x.Id).ToListAsync();

            // mark model items as favorite
            model.Where(item => favPlacesId.Contains(item.Id)).ToList().ForEach(x => x.IsUserFavorite = true);
            // add distance to model
            model.ForEach(x => x.DistanceFromUser = SearchNearbyPlaces.DistanceBetweenPlaces(x.Latitude, x.Longitude, user.Latitude, user.Longitude));
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddToFavs([FromBody] ReturnString request)
        {
            var place = _placesService.GetById(request.Id);
            if (_userService.AddFavPlace(place))
            {
                return Json(new { success = true, responseText = "Dodano do ulubionych!", isAdded = true });
            }
            _userService.RemoveFavPlace(place);
            return Json(new { success = true, responseText = "Usunięto z ulubionych!", isAdded = false });
        }


        [Authorize]
        public ActionResult Details(int id)
        {
            var model = _placesService.GetAllQueryable().Where(x => x.Id == id).Include(x => x.Tasks).SingleOrDefault();
            var modelMapped = _mapper.Map<PlaceViewModel>(model);

            ViewBag.IsUserFavorite = _userService.GetFavPlaces().Any(x => x.Id.Equals(id));
            ViewBag.HasAnyActiveTasks = model != null && model.Tasks.Any();
            return View(modelMapped);
        }


        [Authorize]
        public IActionResult Edit(int id)
        {
            var userId = _userService.GetCurrentUserId();
            var place = _placesService.GetById(id);
            if (place.CreatedById != userId)
            {
                return Json(new { success = false, responseText = "Tylko twórca może edytować miejsce"});
            } 
            var model = _mapper.Map<Place, PlaceEditModel>(place);


            return View("Edit", model);
        }


        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, PlaceEditModel model, IFormFile file)
        {
            var place = _placesService.GetById(id);
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                place = _mapper.Map<PlaceEditModel, Place>(model, place);
                if (file != null) { place.Photo = ImageConverter.ConvertImage(file, out string message); }
                _placesService.Update(place);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [Authorize]
        public IActionResult Delete(int id)
        {
            _placesService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
