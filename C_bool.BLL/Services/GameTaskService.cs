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

            //pojawienie się informacji dla użytkownika, że pojawiło mu się zadanie do zatwierdzenia. (stworzyć klasę)
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

            if (gameTask.Type == TaskType.TextEntry)
            {
                if (isActive == gameTask.IsActive == true && textToConfirm.ToLower().Contains(gameTask.TextCriterion.ToLower()))
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
            //flaga do zadania. ->MaxCount
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
            if (gameTask.Type == TaskType.CheckInAtTheSpecifiedTime)
            {
                if (gameTask.IsActive == true && timeOfVisit.Date.ToString("d") == gameTask.ValidThru.Date.ToString("d"))
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