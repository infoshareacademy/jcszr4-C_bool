using System;
using AutoMapper;
using C_bool.BLL.DAL.Context;
using C_bool.BLL.DAL.Entities;
using C_bool.BLL.Enums;
using C_bool.BLL.Repositories;
using C_bool.BLL.Services;
using C_bool.WebApp.Models;
using C_bool.WebApp.Models.GameTask;
using C_bool.WebApp.Models.Place;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace C_bool.WebApp.Controllers
{
    public class GameTasksController : Controller
    {
        private readonly ILogger<GameTasksController> _logger;
        private readonly IPlaceService _placesService;
        private readonly IUserService _userService;
        private readonly IGameTaskService _gameTaskService;

        private readonly IMapper _mapper;



        public GameTasksController(
            ILogger<GameTasksController> logger,
            IMapper mapper,
            IPlaceService placesService,
            IUserService usersService,
            IGameTaskService gameTaskService
        )
        {
            _logger = logger;
            _mapper = mapper;
            _placesService = placesService;
            _userService = usersService;
            _gameTaskService = gameTaskService;
        }

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult Details(int id)
        {
            return View();
        }

        [Authorize]
        public ActionResult ChooseToCreate(int placeId)
        {
            var model = _mapper.Map<PlaceViewModel>(_placesService.GetPlaceById(placeId));
            ViewData["PlaceId"] = placeId;
            return View(model);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChooseToCreate(int placeId, string taskType)
        {
            Enum.TryParse(taskType, true, out TaskType typeEnum);
            try
            {
                return RedirectToAction("Create", new { placeId, taskType});
            }
            catch
            {
                return View();
            }
        }


        [Authorize]
        public ActionResult Create(int placeId, string taskType)
        {
            Enum.TryParse(taskType, true, out TaskType typeEnum);

            if (typeEnum == TaskType.CheckInToALocation) { }


            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GameTaskViewModel model, IFormFile file)
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

        [Authorize]
        public ActionResult Edit(int id)
        {
            return View();
        }

        [Authorize]
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

        [Authorize]
        public ActionResult Delete(int id)
        {
            return View();
        }

        [Authorize]
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
