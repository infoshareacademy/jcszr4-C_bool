using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using AutoMapper;
using C_bool.BLL.DAL.Context;
using C_bool.BLL.DAL.Entities;
using C_bool.BLL.Enums;
using C_bool.BLL.Repositories;
using C_bool.BLL.Services;
using C_bool.WebApp.Helpers;
using C_bool.WebApp.Models;
using C_bool.WebApp.Models.GameTask;
using C_bool.WebApp.Models.Place;
using Castle.Core.Internal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
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

        private IRepository<GameTask> _gameTasksRepository;
        private IRepository<UserGameTask> _userGameTasksRepository;
        private IRepository<Place> _placesRepository;

        private readonly IMapper _mapper;

        public GameTasksController(
            ILogger<GameTasksController> logger,
            IMapper mapper,
            IPlaceService placesService,
            IUserService usersService,
            IGameTaskService gameTaskService,
            IRepository<GameTask> gameTasksRepository,
            IRepository<Place> placesRepository, 
            IRepository<UserGameTask> userGameTasksRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _placesService = placesService;
            _userService = usersService;
            _gameTaskService = gameTaskService;
            _gameTasksRepository = gameTasksRepository;
            _placesRepository = placesRepository;
            _userGameTasksRepository = userGameTasksRepository;
        }

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize] 
        public ActionResult Details(int gameTaskId)
        {
            var user = _userService.GetCurrentUser();
            ViewBag.Latitude = user.Latitude;
            ViewBag.Longitude = user.Longitude;
            ViewData["IsInUserGameTasks"] = _gameTaskService.GetUserGameTaskByIds(user.Id, gameTaskId) != null;
            //TODO: jakieś dziwne rzeczy, samo val model nie pobiera dodatkowo miejsca w propertisach, ale jak się wyżej wywoła niepowiązane placeGameTask, to już jest...
            //var placeGameTask = _placesRepository.GetAllQueryable().FirstOrDefault(x => x.Tasks.Any<GameTask>(y => gameTaskId.Equals(y.Id)));
            //var model = _gameTasksRepository.GetById(gameTaskId);
            var model = _gameTasksRepository
                .GetAllQueryable()
                .Where(x => x.Id == gameTaskId)
                .Include(x => x.Place)
                .FirstOrDefault();

            return View(model);
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

            ViewData["PlaceId"] = placeId;
            ViewData["TaskType"] = typeEnum;

            if (typeEnum == TaskType.CheckInToALocation) { }

            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GameTaskEditModel model, int placeId, string taskType, IFormFile file)
        {
            //for model invalid state or exception - properties used in view
            Enum.TryParse(taskType, true, out TaskType typeEnum);
            ViewData["PlaceId"] = placeId;
            ViewData["TaskType"] = typeEnum;

            var gameTaskModel = _mapper.Map<GameTaskEditModel, GameTask>(model);
            try
            {
                if (model.Type == TaskType.TextEntry && model.TextCriterion.IsNullOrEmpty())
                {
                    ModelState.AddModelError("TextCriterion", "Dla tego typu zadania musisz podać tajemne hasło");
                }

                if (!ModelState.IsValid)
                {
                    ViewBag.Message = new StatusMessage($"Nie udało się utworzyć nowego zadania", StatusMessage.Status.FAIL);
                    return View(model);
                }

                if (gameTaskModel.LeftDoneAttempts > 0)
                {
                    gameTaskModel.IsDoneLimited = true;
                }

                var place = _placesService.GetPlaceById(placeId);
                gameTaskModel.Place = place;
                gameTaskModel.Photo = ImageConverter.ConvertImage(file, out string message);
                gameTaskModel.CreatedByName = _userService.GetCurrentUser().Email;
                gameTaskModel.CreatedById = _userService.GetCurrentUserId().ToString();
                _gameTasksRepository.Add(gameTaskModel);
                place.Tasks.Add(gameTaskModel);
                _placesRepository.Update(place);

                ViewBag.Message = new StatusMessage($"Dodano nowe miejsce: {gameTaskModel.Name}", StatusMessage.Status.INFO);
                return RedirectToAction("Details", new { gameTaskId = gameTaskModel.Id });
            }
            catch (Exception ex)
            {
                ViewBag.Message = new StatusMessage($"Błąd: {ex.Message}", StatusMessage.Status.FAIL);
                return View(model);
            }
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            var model = _mapper.Map<GameTask, GameTaskEditModel>(_gameTasksRepository.GetById(id));


            return View("Edit", model);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, GameTaskEditModel model, IFormFile file)
        {
            var gameTask = _gameTasksRepository
                .GetAllQueryable()
                .Where(x => x.Id == id)
                .Include(x => x.Place)
                .Include(x => x.UserGameTasks)
                .FirstOrDefault();

            model.Type = gameTask.Type;
            try
            {
                if (model.Type == TaskType.TextEntry && model.TextCriterion.IsNullOrEmpty())
                {
                    ModelState.AddModelError("TextCriterion", "Dla tego typu zadania musisz podać tajemne hasło");
                }

                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                if (model.LeftDoneAttempts > 0)
                {
                    model.IsDoneLimited = true;
                }

                gameTask = _mapper.Map<GameTaskEditModel, GameTask>(model, gameTask);
                if (file != null) gameTask.Photo = ImageConverter.ConvertImage(file, out string message);
                _gameTasksRepository.Update(gameTask);
                return RedirectToAction("Details", new { gameTaskId = gameTask.Id });
            }
            catch (Exception ex)
            {
                ViewBag.Message = new StatusMessage($"Błąd: {ex.Message}", StatusMessage.Status.FAIL);
                return View();
            }
        }

        [Authorize]
        public ActionResult Participate(int id)
        {
            var user = _userService.GetCurrentUser();
            ViewBag.Latitude = user.Latitude;
            ViewBag.Longitude = user.Longitude;

            var gameTask = _gameTasksRepository.GetAllQueryable().Where(x => x.Id == id).Include(x => x.Place).SingleOrDefault();
            var model = _mapper.Map<GameTask, GameTaskViewModel>(gameTask);
            return View(model);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Participate(int gameTaskId, GameTaskParticipateModel model, IFormFile file)
        {
            bool status = false;
            string message = string.Empty;
            string photoMessage = string.Empty;
            var user = _userService.GetCurrentUser();
            var userGameTask = _userGameTasksRepository
                .GetAllQueryable()
                .Where(x => x.GameTaskId == gameTaskId && x.UserId == user.Id)
                .Include(x => x.User)
                .Include(x => x.GameTask)
                .FirstOrDefault();

            userGameTask.ArrivalTime = DateTime.Now;

            if (userGameTask.GameTask.Type == TaskType.TextEntry)
            {
                userGameTask.TextCriterion = model.UserTextCriterion;
            }
            if (userGameTask.GameTask.Type == TaskType.TakeAPhoto)
            {
                if (file == null) ModelState.AddModelError("Photo", "Nie wybrałeś zdjęcia");
                else userGameTask.Photo = ImageConverter.ConvertImage(file, out photoMessage);
                if (userGameTask.Photo == null) ModelState.AddModelError("Photo", "Zdjęcie jest nieprawidłowego formatu");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Message = new StatusMessage($"Błąd: {photoMessage}", StatusMessage.Status.FAIL);
                return RedirectToAction("Participate", new { id = gameTaskId });
            }

            _userGameTasksRepository.Update(userGameTask);
            if (userGameTask.GameTask.Type == TaskType.TakeAPhoto) _gameTaskService.ManuallyCompleteTask(gameTaskId, user.Id);
            else _gameTaskService.CompleteTask(gameTaskId, user.Id, out message);

            return Json(new { success = status, responseText = message });
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddToFavs([FromBody] ReturnString request)
        {
            var gameTask = _gameTasksRepository.GetById(int.Parse(request.Id));
            if (_userService.AddTaskToUser(gameTask))
            {
                return Json(new { success = true, responseText = "Dodano do twoich zadań!", isAdded = true });
            }
            return Json(new { success = true, responseText = "Te zadanie jest już w Twojej kolejce!", isAdded = true });
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
