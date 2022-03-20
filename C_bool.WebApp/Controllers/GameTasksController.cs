
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using C_bool.BLL.DAL.Entities;
using C_bool.BLL.Enums;
using C_bool.BLL.Logic;
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

namespace C_bool.WebApp.Controllers
{
    public class GameTasksController : Controller
    {
        private readonly ILogger<GameTasksController> _logger;
        private readonly IPlaceService _placesService;
        private readonly IUserService _userService;
        private readonly IGameTaskService _gameTaskService;
        private readonly IReportService _reportService;
        private readonly IEmailSenderService _emailSenderService;


        private readonly IMapper _mapper;

        public GameTasksController(
            ILogger<GameTasksController> logger,
            IMapper mapper,
            IPlaceService placesService,
            IUserService usersService,
            IGameTaskService gameTaskService,
            IEmailSenderService emailSenderService, IReportService reportService)
        {
            _logger = logger;
            _mapper = mapper;
            _placesService = placesService;
            _userService = usersService;
            _gameTaskService = gameTaskService;
            _emailSenderService = emailSenderService;
            _reportService = reportService;
        }

        [Authorize]
        public async Task<IActionResult> Index(
            string searchString,
            string taskType,
            string sortBy,
            bool sortOrder,
            bool onlyUserFavs,
            bool onlyActive,
            bool onlyNotDone,
            bool all,
            bool userCreated,
            bool completed,
            int placeId,
            double range)
        {
            var user = _userService.GetCurrentUser();
            ViewBag.Latitude = user.Latitude;
            ViewBag.Longitude = user.Longitude;

            if (range == 0) range = 40000000;

            ViewBag.Range = range / 1000;

            ViewData["searchString"] = searchString;
            ViewData["taskType"] = taskType;
            ViewData["sortBy"] = sortBy;
            ViewData["sortOrder"] = sortOrder;
            ViewData["onlyUserFavs"] = onlyUserFavs;
            ViewData["onlyActive"] = onlyActive;
            ViewData["onlyNotDone"] = onlyNotDone;
            ViewData["userCreated"] = userCreated;
            ViewData["completed"] = completed;
            ViewData["range"] = range;
            ViewData["MapZoom"] = range;
            ViewData["IsInPlaceView"] = false;

            var placesWithTasksIds = await _placesService.GetNearbyPlacesQueryable(user.Latitude, user.Longitude, range).Where(p => p.Tasks.Any()).Select(x => x.Id).ToListAsync();

            var tasks = _gameTaskService.GetAllQueryable().Where(x => placesWithTasksIds.Contains(x.Place.Id));

            //search queries, based on user input
            if (!string.IsNullOrEmpty(searchString))
            {
                tasks = tasks.Where(x => x.Name.Contains(searchString) || x.ShortDescription.Contains(searchString) || x.Place.Name.Contains(searchString) || x.Place.Address.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(taskType) && taskType != "0")
            {
                Enum.TryParse(taskType, true, out TaskType type);
                tasks = tasks.Where(x => x.Type == type);
            }

            if (onlyActive)
            {
                tasks = tasks.Where(p => p.IsActive).Where(x => x.ValidThru >= DateTime.UtcNow || x.ValidThru == DateTime.MinValue);
            }

            if (onlyUserFavs)
            {
                tasks = tasks.Where(x => _userService.GetAllTasks().Contains(x));
            }

            if (onlyNotDone)
            {
                tasks = tasks.Where(x => !_userService.GetDoneTasks().Contains(x));
            }

            if (completed)
            {
                tasks = tasks.Where(x => _userService.GetDoneTasks().Contains(x));
            }

            if (!all) tasks = userCreated ? tasks.Where(x => x.CreatedById == user.Id) : tasks.Where(x => x.CreatedById != user.Id);

            // sort order
            tasks = sortBy switch
            {
                "name" => tasks.OrderBy(s => s.Name),
                "active" => tasks.OrderBy(s => s.IsActive),
                "from_date" => tasks.OrderBy(s => s.ValidFrom),
                "to_date" => tasks.OrderBy(s => s.ValidThru),
                "create_date" => tasks.OrderBy(s => s.CreatedOn),
                "type" => tasks.OrderBy(s => s.Type),
                _ => tasks.OrderBy(s => s.IsActive)
            };

            var model = _mapper.Map<List<GameTaskViewModel>>(tasks.AsNoTracking().Include(x => x.Place));
            await AssignViewModelProperties(model);

            model = sortBy switch
            {
                "distance" => model.OrderBy(x => x.DistanceFromUser).ToList(),
                _ => model
            };

            if (sortOrder) model.Reverse();

            ViewBag.PlacesCount = tasks.Count();

            ViewBag.Message = new StatusMessage($"Znaleziono {model.ToList().Count} pasujących zadań", StatusMessage.Status.INFO);
            return View(model);
        }

        [Authorize]
        public async Task<ActionResult> Details(int gameTaskId)
        {
            var user = _userService.GetCurrentUser();
            ViewBag.Latitude = user.Latitude;
            ViewBag.Longitude = user.Longitude;

            // if user already completed task - get different view
            if (_userService.GetDoneTasks(user.Id).Any(x => x.Id == gameTaskId))
            {
                return RedirectToAction("TaskDoneDetails", new { id = gameTaskId });
            }

            // get model
            var model = await _gameTaskService
                .GetAllQueryable()
                .Where(x => x.Id == gameTaskId)
                .Include(x => x.Place)
                .FirstOrDefaultAsync();

            // return details view or redirect to "participate" action

            if (model != null && model.CreatedById == user.Id) return RedirectToAction("UserCreatedDetails", new { id = gameTaskId });

            if (model != null) return RedirectToAction("Participate", new { id = gameTaskId });

            var error = new CustomErrorModel
            {
                RequestId = Request.Headers["RequestId"],
                Title = "Coś poszło nie tak...",
                Message = "Zadanie o podanym ID nie zostało znalezione"
            };
            _logger.LogWarning("User {userId}, tried to show details to incorrect GameTask with id:{gameTaskId}", user.Id, gameTaskId);
            return View("CustomError", error);

        }

        [Authorize]
        public async Task<ActionResult> UserCreatedDetails(int id)
        {
            var user = _userService.GetCurrentUser();
            ViewBag.Latitude = user.Latitude;
            ViewBag.Longitude = user.Longitude;


            // get model
            var model = await _gameTaskService
                .GetAllQueryable()
                .Where(x => x.Id == id)
                .Include(x => x.Place)
                .FirstOrDefaultAsync();


            if (model != null && model.CreatedById == user.Id) return View("Details", model);


            var error = new CustomErrorModel
            {
                RequestId = Request.Headers["RequestId"],
                Title = "Coś poszło nie tak...",
                Message = "Nie masz uprawnień do wyświetlenia tego zadania w trybie twórcy"
            };
            _logger.LogWarning("User {userId}, tried to show details to incorrect GameTask with id:{gameTaskId}", user.Id, id);
            return View("CustomError", error);

        }

        [Authorize]
        public async Task<ActionResult> TaskDoneDetails(int id)
        {

            var user = _userService.GetCurrentUser();

            // if user already completed task - get different view
            if (_userService.GetDoneTasks(user.Id).Any(x => x.Id == id))
            {
                var userGameTask = await _gameTaskService
                    .GetAllUserGameTasksQueryable()
                    .Where(x => x.GameTaskId == id && x.UserId == user.Id)
                    .Include(x => x.User)
                    .Include(x => x.GameTask)
                    .ThenInclude(x => x.Place)
                    .FirstOrDefaultAsync();

                var userGameTaskModel = _mapper.Map<UserGameTaskViewModel>(userGameTask);
                await AssignViewModelProperties(userGameTaskModel.GameTask);
                return View("AfterDone/TaskDone", userGameTaskModel);
            }

            var error = new CustomErrorModel
            {
                RequestId = Request.Headers["RequestId"],
                Title = "Nie znaleziono zadania...",
                Message = "Zadanie o podanym ID nie zostało znalezione"
            };
            _logger.LogWarning("User {userId}, tried to show details to incorrect GameTask with id:{gameTaskId}", user.Id, id);
            return View("CustomError", error);

        }

        [Authorize]
        public ActionResult ChooseToCreate(int placeId)
        {
            var model = _mapper.Map<PlaceViewModel>(_placesService.GetById(placeId));
            ViewData["PlaceId"] = placeId;
            return View(model);
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
        public async Task<ActionResult> Edit(int id)
        {
            var task = _gameTaskService.GetById(id);
            var userId = _userService.GetCurrentUserId();
            var roles = await _userService.GetUserRoles();
            if (task.CreatedById != userId)
            {
                if (!roles.Contains("Admin") || roles.Contains("Moderator"))
                {
                    var error = new CustomErrorModel
                    {
                        RequestId = Request.Headers["RequestId"],
                        Title = "Nie możesz edytować tego zadania",
                        Message = "Tylko autor zadania, administrator lub moderator może edytować zadanie"
                    };
                    _logger.LogWarning("User {userId}, tried to edit properties of GameTask with id:{gameTaskId}, but was not creator or Admin", userId, id);
                    return View("CustomError", error);
                }
            }
            var model = _mapper.Map<GameTask, GameTaskEditModel>(task);


            return View("Edit", model);
        }

        [Authorize]
        public async Task<ActionResult> Participate(int id)
        {
            var user = _userService.GetCurrentUser();
            ViewBag.Latitude = user.Latitude;
            ViewBag.Longitude = user.Longitude;

            var gameTask = _gameTaskService.GetAllQueryable().Where(x => x.Id == id).Include(x => x.Place).SingleOrDefault();
            if (gameTask == null)
            {
                var error = new CustomErrorModel
                {
                    RequestId = Request.Headers["RequestId"],
                    Title = "Coś poszło nie tak...",
                    Message = "Zadanie o podanym ID nie zostało znalezione"
                };
                return View("CustomError", error);
            }
            if (gameTask.CreatedById == user.Id)
            {
                var error = new CustomErrorModel
                {
                    RequestId = Request.Headers["RequestId"],
                    Title = "Nie możesz uczestniczyć w tym zadaniu",
                    Message = "Twórca nie może uczestniczyć we własnych zadaniach, idź pobawić się gdzieś indziej :-) "
                };
                _logger.LogWarning("User {userId}, tried to participate in own GameTask with id:{gameTaskId}", user.Id, id);
                return View("CustomError", error);
            }

            var model = _mapper.Map<GameTask, GameTaskViewModel>(gameTask);
            await AssignViewModelProperties(model);
            return View(model);
        }

        [Authorize]
        public ActionResult AfterDoneSplashScreen(int gameTaskId)
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
                    Title = "Brak zadania do zaliczenia",
                    Message = "Zadanie zostało już zaliczone lub nie masz uprawnień do zaliczenia tego zadania"
                };
                _logger.LogWarning("User {userId} tried to approve submission of UserGameTask with id:{gameTaskId}, but has no permission", user.Id, gameTaskId);
                return View("CustomError", viewMessageOnNoTask);
            }

            var task = taskToApprove.FirstOrDefault();

            GameTaskStatus status = _gameTaskService.ManuallyCompleteTask(task.GameTaskId, userToApproveId, extraPoints, out message);

            if (status == GameTaskStatus.Done)
            {
                var messageToSend = new Message(user.Id, user.UserName,
                    $"Zadanie {task.GameTask.Name} zaliczone! Zdobyłeś {task.GameTask.Points} punktów!", HtmlRenderer.CheckTaskPhoto(task));

                messageToSend.Type = MessageType.SubmissionApproval;

                _userService.PostMessage(userToApproveId, messageToSend);

                var viewMessageOk = new CustomErrorModel
                {
                    RequestId = Request.Headers["RequestId"],
                    Title = "Zadanie zaliczone!",
                    Message = "Zatwierdziłeś rozwiązanie tego zadania!",
                    Type = CustomErrorModel.MessageType.Success
                };
                
                return View("CustomError", viewMessageOk);

            }
            var viewMessageOnError = new CustomErrorModel
            {
                RequestId = Request.Headers["RequestId"],
                Title = "Brak zadania do zaliczenia",
                Message = "Zadanie nie zostało znalezione"
            };
            return View("CustomError", viewMessageOnError);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChooseToCreate(int placeId, string taskType)
        {
            var parseStatus = Enum.TryParse(taskType, true, out TaskType typeEnum);
            if (parseStatus)
            {
                return RedirectToAction("Create", new { placeId, taskType });
            }
            _logger.LogError("Cannot continue to create new GameTask - task type {taskType} is not valid", taskType);
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

                ViewBag.Message = new StatusMessage($"Dodano nowe zadanie: {gameTaskModel.Name}", StatusMessage.Status.INFO);
                return RedirectToAction("Details", new { gameTaskId = gameTaskModel.Id });
            }
            catch (Exception ex)
            {
                _logger.LogError("Error when creating GameTask: {exceptionMessage}", ex.Message, gameTaskModel);
                ViewBag.Message = new StatusMessage($"Błąd: {ex.Message}", StatusMessage.Status.FAIL);
                return View(model);
            }
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
                _logger.LogError("Error editing gametask with Id {gameTaskId}: {exceptionMessage.Message}", id, ex, model);
                ViewBag.Message = new StatusMessage($"Błąd: {ex.Message}", StatusMessage.Status.FAIL);
                return View();
            }
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Participate(int gameTaskId, GameTaskParticipateModel model, IFormFile file)
        {
            try
            {
                var user = _userService.GetCurrentUser();
                var userGameTask = _gameTaskService.GetUserGameTaskByIds(user.Id, gameTaskId);
                if (userGameTask == null)
                {
                    _userService.AddTaskToUser(_gameTaskService.GetById(gameTaskId));
                }
                var base64Photo = ImageConverter.ConvertImage(file, out string photoMessage);
                if (userGameTask.GameTask.Type == TaskType.TakeAPhoto && base64Photo.IsNullOrEmpty())
                {
                    var error = new CustomErrorModel
                    {
                        RequestId = Request.Headers["RequestId"],
                        Title = "Nieprawidłowe zdjęcie...",
                        Message = $"Nie wstawiłeś zdjęcia z rozwiązaniem lub zdjęcie ma nieprawidłowy format: {photoMessage}"
                    };
                    return View("CustomError", error);
                }
                _gameTaskService.AssignPropertiesFromParticipateModel(userGameTask, model.UserTextCriterion,
                    base64Photo);
                GameTaskStatus status = _gameTaskService.CompleteTask(gameTaskId, user.Id, out string message);
                if (status == GameTaskStatus.InReview)
                {
                    var messageToSend = new Message(user.Id, user.UserName,
                        $"Zatwierdzenie zadania: {userGameTask.GameTask.Name}",
                        HtmlRenderer.CheckTaskPhoto(userGameTask));
                    messageToSend.Type = MessageType.SubmissionRequest;
                    _emailSenderService.SendCheckPhotoEmail(userGameTask, messageToSend);
                    _userService.PostMessage(userGameTask.GameTask.CreatedById, messageToSend);
                    return View("AfterDone/WaitForApproval");
                }
                if (status == GameTaskStatus.NotDone)
                {
                    var error = new CustomErrorModel
                    {
                        RequestId = Request.Headers["RequestId"],
                        Title = "Zadanie nie zaliczone...",
                        Message = message
                    };
                    return View("CustomError", error);
                }
                return RedirectToAction("AfterDoneSplashScreen", new { gameTaskId = userGameTask.GameTask.Id });
            }
            catch (Exception ex)
            {
                var error = new CustomErrorModel
                {
                    RequestId = Request.Headers["RequestId"],
                    Title = "Coś poszło nie tak...",
                    Message = ex.Message
                };
                _logger.LogError("Error while trying to set task as done for UserGameTask id:{gameTaskId}: {exceptionMessage.Message}", gameTaskId, ex);
                return View("CustomError", error);
            }
        }

        [Authorize]
        [HttpGet]
        public ActionResult ChangeActiveStatus(int id, bool active)
        {
            var user = _userService.GetCurrentUser();
            var gameTask = _gameTaskService.GetAllQueryable()
                .Where(x => x.CreatedById == user.Id)
                .FirstOrDefault(x => x.Id == id);

            if (gameTask != null)
            {
                gameTask.IsActive = active;
                _gameTaskService.Update(gameTask);

                var viewMessageOk = new CustomErrorModel
                {
                    RequestId = Request.Headers["RequestId"],
                    Title = "Zmieniłeś status aktywności zadania",
                    Message = $"Zmieniłeś status zadania na {(active ? "aktywny, inni użytkownicy mogą teraz do niego przystąpić." : "nieaktywny, od teraz żaden użytkownik nie może go zaliczyć.")}",
                    Type = CustomErrorModel.MessageType.Success
                };
                return View("CustomError", viewMessageOk);
            }
            var viewMessageOnError = new CustomErrorModel
            {
                RequestId = Request.Headers["RequestId"],
                Title = "Nie znaleziono zadania",
                Message = "Zadanie nie zostało znalezione lub nie masz uprawnień do zmiany tego zadania"
            };
            return View("CustomError", viewMessageOnError);
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddToFavs([FromBody] ReturnString request)
        {
            var gameTask = _gameTaskService.GetById(int.Parse(request.Id));
            if (_userService.AddTaskToUser(gameTask))
            {
                return Json(new { success = true, responseText = "Dodano do twoich zadań!", isAdded = true });
            }
            return Json(new { success = true, responseText = "Te zadanie jest już w Twojej kolejce!", isAdded = true });
        }
        #region private methods

        private async Task AssignViewModelProperties(GameTaskViewModel model)
        {
            var user = _userService.GetCurrentUser();
            var favTasksId = await _gameTaskService.GetAllQueryable().Where(p => _userService.GetAllTasks().Contains(p)).Select(x => x.Id).ToListAsync();
            var completedTasksId = _userService.GetDoneTasks(user.Id).Select(x => x.Id);

            // is created by current user
            if (model.CreatedById == user.Id)
                model.IsUserCreated = true;
            // is user favorite
            if (favTasksId.Contains(model.Id))
                model.IsUserFavorite = true;
            // is user completed
            if (completedTasksId.Contains(model.Id))
                model.IsUserCompleted = true;
            // is overdue
            if (model.ValidThru != DateTime.MinValue && model.ValidThru < DateTime.UtcNow)
                model.IsOverdue = true;

            // distance from user
            model.DistanceFromUser = SearchNearbyPlaces.DistanceBetweenPlaces(model.Place.Latitude, model.Place.Longitude, user.Latitude, user.Longitude);
        }

        private async Task AssignViewModelProperties(List<GameTaskViewModel> model)
        {
            var user = _userService.GetCurrentUser();
            var favTasksId = await _gameTaskService.GetAllQueryable().Where(p => _userService.GetAllTasks().Contains(p)).Select(x => x.Id).ToListAsync();
            var completedTasksId = _userService.GetDoneTasks(user.Id).Select(x => x.Id);

            // is created by current user
            model.Where(item => item.CreatedById == user.Id).ToList().ForEach(x => x.IsUserCreated = true);
            // is user favorite
            model.Where(item => favTasksId.Contains(item.Id)).ToList().ForEach(x => x.IsUserFavorite = true);
            // is user completed
            model.Where(item => completedTasksId.Contains(item.Id)).ToList().ForEach(x => x.IsUserCompleted = true);
            // is overdue
            model.Where(item => item.ValidThru != DateTime.MinValue && item.ValidThru < DateTime.UtcNow).ToList().ForEach(x => x.IsOverdue = true);
            // distance from user
            model.ForEach(x => x.DistanceFromUser = SearchNearbyPlaces.DistanceBetweenPlaces(x.Place.Latitude, x.Place.Longitude, user.Latitude, user.Longitude));
        }

        #endregion
    }
}
