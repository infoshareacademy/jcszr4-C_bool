using AutoMapper;
using C_bool.BLL.DAL.Entities;
using C_bool.BLL.Enums;
using C_bool.BLL.Services;
using C_bool.WebApp.Helpers;
using C_bool.WebApp.Models;
using C_bool.WebApp.Models.GameTask;
using C_bool.WebApp.Models.Place;
using Castle.Core.Internal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//todo: przeniesc repo do serwisów
namespace C_bool.WebApp.Controllers
{
    public class GameTasksController : Controller
    {
        private readonly ILogger<GameTasksController> _logger;
        private readonly IPlaceService _placesService;
        private readonly IUserService _userService;
        private readonly IGameTaskService _gameTaskService;
        private readonly IEmailSenderService _emailSenderService;


        private readonly IMapper _mapper;

        public GameTasksController(
            ILogger<GameTasksController> logger,
            IMapper mapper,
            IPlaceService placesService,
            IUserService usersService,
            IGameTaskService gameTaskService,
            IEmailSenderService emailSenderService
            )
        {
            _logger = logger;
            _mapper = mapper;
            _placesService = placesService;
            _userService = usersService;
            _gameTaskService = gameTaskService;
            _emailSenderService = emailSenderService;
        }

        [Authorize]
        public async Task<IActionResult> Index(string searchString, bool searchOnlyFavs, bool searchOnlyWithTasks, double range)
        {
            var user = _userService.GetCurrentUser();
            ViewBag.Latitude = user.Latitude;
            ViewBag.Longitude = user.Longitude;

            if (range == 0) range = 100000;

            ViewBag.Range = range / 1000;

            ViewData["CurrentFilter"] = searchString;
            ViewData["OnlyFavs"] = searchOnlyFavs;
            ViewData["OnlyTask"] = searchOnlyWithTasks;
            ViewData["CurrentRange"] = range;
            ViewData["MapZoom"] = range;

            var placesWithTasksIds = await _placesService.GetNearbyPlacesQueryable(user.Latitude, user.Longitude, range).Where(p => p.Tasks.Any()).Select(x => x.Id).ToListAsync();

            var tasks = _gameTaskService.GetAllQueryable().Where(x => placesWithTasksIds.Contains(x.Place.Id));

            //search queries, based on user input
            if (!string.IsNullOrEmpty(searchString))
            {
                tasks = tasks.Where(x => x.Name.Contains(searchString) || x.ShortDescription.Contains(searchString) || x.Place.Name.Contains(searchString) || x.Place.Address.Contains(searchString));
            }


            if (searchOnlyFavs)
            {
                if (user.UserGameTasks != null)
                {
                    tasks = tasks.Where(x => _userService.GetAllTasks().Contains(x));
                }
            }

            if (searchOnlyWithTasks)
            {
                tasks = tasks.Where(p => p.IsActive);
            }

            var model = _mapper.Map<List<GameTaskViewModel>>(tasks.AsNoTracking().Include(x => x.Place));


            ViewBag.PlacesCount = tasks.Count();

            ViewBag.Message = new StatusMessage($"Znaleziono {model.ToList().Count} pasujących miejsc", StatusMessage.Status.INFO);
            return View(model);
        }

        [Authorize]
        public ActionResult Details(int gameTaskId)
        {
            var user = _userService.GetCurrentUser();
            ViewBag.Latitude = user.Latitude;
            ViewBag.Longitude = user.Longitude;

            // if user already completed task - get different view
            if (_userService.GetDoneTasks(user.Id).Any(x => x.Id == gameTaskId))
            {
                var userGameTask = _gameTaskService
                    .GetAllUserGameTasksQueryable()
                    .Where(x => x.GameTaskId == gameTaskId && x.UserId == user.Id)
                    .Include(x => x.User)
                    .Include(x => x.GameTask)
                    .ThenInclude(x => x.Place)
                    .FirstOrDefault();

                return View("AfterDone/TaskDone", userGameTask);
            }

            // check if task is in user list - different buttons on view
            ViewData["IsInUserGameTasks"] = _gameTaskService.GetUserGameTaskByIds(user.Id, gameTaskId) != null;

            var model = _gameTaskService
                .GetAllQueryable()
                .Where(x => x.Id == gameTaskId)
                .Include(x => x.Place)
                .FirstOrDefault();

            return View(model);
        }

        [Authorize]
        public ActionResult ChooseToCreate(int placeId)
        {
            var model = _mapper.Map<PlaceViewModel>(_placesService.GetById(placeId));
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
                return RedirectToAction("Create", new { placeId, taskType });
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

                //assign necessary properties to newly created GameTask object & update repository
                var place = _placesService.GetById(placeId);
                gameTaskModel.Place = place;
                gameTaskModel.Photo = ImageConverter.ConvertImage(file, out string message);
                gameTaskModel.CreatedByName = _userService.GetCurrentUser().UserName;
                gameTaskModel.CreatedById = _userService.GetCurrentUserId();
                _gameTaskService.Add(gameTaskModel);

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
        public async Task<ActionResult> Edit(int id)
        {
            var task = _gameTaskService.GetById(id); ;
            var roles = await _userService.GetUserRoles();
            if (task.CreatedById != _userService.GetCurrentUserId())
            {
                if (!roles.Contains("Admin") || roles.Contains("Moderator"))
                {
                    var error = new CustomErrorModel
                    {
                        RequestId = Request.Headers["RequestId"],
                        Title = "Cannot complete operation",
                        Message = "Only the author, administrator or moderator can edit the content of a task"
                    };
                    return View("CustomError", error);
                }
            }
            var model = _mapper.Map<GameTask, GameTaskEditModel>(task);


            return View("Edit", model);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, GameTaskEditModel model, IFormFile file)
        {
            var gameTask = _gameTaskService
                .GetAllQueryable()
                .Where(x => x.Id == id)
                .Include(x => x.Place)
                .Include(x => x.UserGameTasks)
                .FirstOrDefault();

            model.Type = gameTask?.Type;
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
                _gameTaskService.Update(gameTask);
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

            var gameTask = _gameTaskService.GetAllQueryable().Where(x => x.Id == id).Include(x => x.Place).SingleOrDefault();
            var model = _mapper.Map<GameTask, GameTaskViewModel>(gameTask);
            return View(model);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Participate(int gameTaskId, GameTaskParticipateModel model, IFormFile file)
        {
            var user = _userService.GetCurrentUser();

            var userGameTask = _gameTaskService.GetUserGameTaskByIds(user.Id, gameTaskId);

            if (userGameTask == null)
            {
                var error = new CustomErrorModel
                {
                    RequestId = Request.Headers["RequestId"],
                    Title = "Something went wrong...",
                    Message = "Task is not assigned to user, you have to add task to your list first"
                };
                return View("CustomError", error);
            }

            var base64Photo = ImageConverter.ConvertImage(file, out string photoMessage);
            if (userGameTask.GameTask.Type == TaskType.TakeAPhoto && base64Photo.IsNullOrEmpty())
            {
                var error = new CustomErrorModel
                {
                    RequestId = Request.Headers["RequestId"],
                    Title = "Something went wrong...",
                    Message = $"No photo submitted or photo is in wrong format, try again: {photoMessage}"
                };
                return View("CustomError", error);
            }

            _gameTaskService.AssignPropertiesFromParticipateModel(userGameTask, model.UserTextCriterion, base64Photo);
            GameTaskStatus status = _gameTaskService.CompleteTask(gameTaskId, user.Id, out string message);

            if (status == GameTaskStatus.InReview)
            {
                var messageToSend = new Message(user.Id, user.UserName,
                    $"Zatwierdzenie zadania: {userGameTask.GameTask.Name}", HtmlRenderer.CheckTaskPhoto(userGameTask));

                _emailSenderService.SendCheckPhotoEmail(userGameTask, messageToSend);

                _userService.PostMessage(userGameTask.GameTask.CreatedById, messageToSend);
                return View("AfterDone/WaitForApproval");
            }

            if (status == GameTaskStatus.NotDone)
            {
                var error = new CustomErrorModel
                {
                    RequestId = Request.Headers["RequestId"],
                    Title = "Task not completed...",
                    Message = message
                };
                return View("CustomError", error);
            }


            return RedirectToAction("TaskDone", new { gameTaskId = userGameTask.GameTask.Id });

        }

        [Authorize]
        public ActionResult TaskDone(int gameTaskId)
        {
            var user = _userService.GetCurrentUser();
            var gameTask = _gameTaskService.GetAllQueryable().FirstOrDefault(x => x.Id == gameTaskId);
            
            ViewData["points"] = gameTask?.Points;
            ViewData["gameTaskId"] = gameTaskId;

            return View("AfterDone/Congratulations");

        }

        [Authorize]
        public ActionResult ApproveUserSubmission(int userToApproveId, int gameTaskId, int extraPoints)
        {
            string message;
            var user = _userService.GetCurrentUser();

            var taskToApprove = _gameTaskService.GetAllUserGameTasksQueryable()
                .Where(x => x.GameTask.CreatedById == user.Id)
                .Where(x => !x.IsDone)
                .Where(x => x.UserId == userToApproveId)
                .Where(x => x.GameTaskId == gameTaskId);

            if (!taskToApprove.Any())
            {
                var viewMessageOnNoTask = new CustomErrorModel
                {
                    RequestId = Request.Headers["RequestId"],
                    Title = "No task to approve",
                    Message = "No task to approve with given criteria or you have no permission to approve this task"
                };
                return View("CustomError", viewMessageOnNoTask);
            }

            var task = taskToApprove.FirstOrDefault();

            GameTaskStatus status = _gameTaskService.ManuallyCompleteTask(task.GameTaskId, userToApproveId, extraPoints, out message);

            if (status == GameTaskStatus.Done)
            {
                var messageToSend = new Message(user.Id, user.UserName,
                    $"Zadanie {task.GameTask.Name} zaliczone! Zdobyłeś {task.GameTask.Points} punktów!", HtmlRenderer.CheckTaskPhoto(task));
                
                _userService.PostMessage(userToApproveId, messageToSend);

                    var viewMessageOk = new CustomErrorModel
                    {
                        RequestId = Request.Headers["RequestId"],
                        Title = "Task approved",
                        Message = "You approved user submission!",
                        Type = CustomErrorModel.MessageType.Success
                    };
                    return View("CustomError", viewMessageOk);
                
            }
            var viewMessageOnError = new CustomErrorModel
            {
                RequestId = Request.Headers["RequestId"],
                Title = "No task to approve",
                Message = "No task to approve with given criteria or you have no permission to approve this task"
            };
            return View("CustomError", viewMessageOnError);
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddToFavs([FromBody] ReturnString request)
        {
            var gameTask = _gameTaskService.GetById(int.Parse(request.Id));
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
