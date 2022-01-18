using System;
using System.Linq;
using C_bool.BLL.DAL.Entities;
using C_bool.BLL.Enums;
using C_bool.BLL.Logic;
using C_bool.BLL.Repositories;

namespace C_bool.BLL.Services
{
    public class GameTaskService : IGameTaskService
    {
        private readonly IRepository<GameTask> _gameTasksRepository;
        private readonly IRepository<User> _userRepository;

        public GameTaskService(IRepository<GameTask> gameTasks, IRepository<User> userRepository)
        {
            _gameTasksRepository = gameTasks;
            _userRepository = userRepository;
        }

        public void ManuallyCompleteTask(int taskId, int userId)
        {
            //jak text entry - tylko manualnie
            //pojawienie się informacji dla użytkownika, że pojawiło mu się zadanie do zatwierdzenia. (stworzyć klasę)

            //dodajemy do UserGameTask - zdjecie stringa
            var user = _userRepository.GetById(userId);
            var gameTask = _gameTasksRepository.GetById(taskId);
            if (gameTask.Type == TaskType.TakeAPhoto)
            {
                if (gameTask.IsActive == true)
                {
                    user.Points += gameTask.Points;
                    user.UserGameTasks.Add(new UserGameTask()
                    {
                        UserId = user.Id,
                        GameTaskId = gameTask.Id,
                        IsDone = true,
                        DoneOn = DateTime.Now
                    });
                }
            }
            _gameTasksRepository.Update(gameTask);
            _userRepository.Update(user);
        }
        public void CompleteTask(int taskId, int userId, string textToConfirm, bool isActive, DateTime timeOfVisit, double latitude, double longitude)
        {
            var user = _userRepository.GetById(userId);
            var userGameTask = _userRepository.GetAllQueryable().Select(u => u.Id).Count();
            var gameTask = _gameTasksRepository.GetById(taskId);

            // nie ma ram czasowych i nie wymaga lokalizacji - tylko okazania dowodu w postaci tekstu
            // zaliczenie automatyczne - taki sam (tylko z malych liter albo duzych albo bez polskich znakow)
            if (gameTask.Type == TaskType.TextEntry)
            {
                if (isActive == gameTask.IsActive == true && textToConfirm.ToLower().Contains(gameTask.TextCriterion.ToLower()))
                {
                    user.Points += gameTask.Points;

                    //z user service - set as done
                    user.UserGameTasks.Add(new UserGameTask()
                    {
                        UserId = user.Id,
                        GameTaskId = gameTask.Id,
                        IsDone = true,
                        DoneOn = DateTime.Now
                    });
                }
            }

            // to zadanie zakłada że twórca określa ilość osób które mogą wykonać zadanie - znaczy się zameldować
            // wywalić ten typ, tylko dodać property do GameTask
            if (gameTask.Type == TaskType.FirstComeFirstServed && userGameTask == 0)
            {
                if (isActive == gameTask.IsActive == true)
                {
                    user.Points += gameTask.Points;
                    user.UserGameTasks.Add(new UserGameTask()
                    {
                        UserId = user.Id,
                        GameTaskId = gameTask.Id,
                        IsDone = true,
                        DoneOn = DateTime.Now
                    });
                }
                gameTask.IsActive = false;
            }
            //TODO:
            //zadanie tego typu ma ustawione ramy czasowe ValidFrom i ValidThru, czyli można to traktować jako wydarzenie
            //np osoba tworzaca zadanie ustawia ze dnia 1 lutego o godzinie 20 -22 jest koncert Zenka, na któym możesz się pojawić
            //tego typu zadania miałyby "kalendarz" na stronie głównej
            if (gameTask.Type == TaskType.Event)
            {
                if (gameTask.IsActive == true && (timeOfVisit.Date <= gameTask.ValidThru.Date) && timeOfVisit.Date > gameTask.ValidFrom)
                {
                    user.Points += gameTask.Points;
                    user.UserGameTasks.Add(new UserGameTask()
                    {
                        UserId = user.Id,
                        GameTaskId = gameTask.Id,
                        IsDone = true,
                        DoneOn = DateTime.Now
                    });
                }
            }

            //to zadanie zakłąda że użytkownik odwiedzi lokalizację i może je zaliczyć tylko jak w niej jest, podobne jak event tylko bez ram czasowych
            if (gameTask.Type == TaskType.CheckInToALocation)
            {
                var range = SearchNearbyPlaces.DistanceBetweenPlaces(user.Latitude, user.Longitude,
                    gameTask.Place.Latitude, gameTask.Place.Longitude);
                if (gameTask.Place != null && isActive == gameTask.IsActive == true && range <= 100)
                {
                    user.Points += gameTask.Points;
                    user.UserGameTasks.Add(new UserGameTask()
                    {
                        UserId = user.Id,
                        GameTaskId = gameTask.Id,
                        IsDone = true,
                        DoneOn = DateTime.Now
                    });
                }
            }
            _gameTasksRepository.Update(gameTask);
            _userRepository.Update(user);
        }
    }
}