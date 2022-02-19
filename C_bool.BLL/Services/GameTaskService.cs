using System;
using System.Linq;
using C_bool.BLL.DAL.Entities;
using C_bool.BLL.Enums;
using C_bool.BLL.Logic;
using C_bool.BLL.Repositories;
using Microsoft.EntityFrameworkCore;

namespace C_bool.BLL.Services
{
    public class GameTaskService : IGameTaskService
    {
        private readonly IRepository<UserGameTask> _userGameTaskRepository;

        public GameTaskService(IRepository<UserGameTask> userGameTaskRepository)
        {
            _userGameTaskRepository = userGameTaskRepository;
        }

        public UserGameTask GetById(int taskId)
        {
            var userGameTask = _userGameTaskRepository.GetAllQueryable();
            return userGameTask.SingleOrDefault(e => e.GameTaskId == taskId);
        }

        public GameTaskStatus CompleteTask(int taskId, int userId, out string message)
        {
            var userGameTask = GetUserGameTaskByIds(userId, taskId);
            message = string.Empty;

            if (!userGameTask.GameTask.IsActive)
            {
                message = "Task is already inactive, cannot continue";
                return GameTaskStatus.NotDone;
            }

            if (userGameTask.GameTask.IsDoneLimited && userGameTask.GameTask.LeftDoneAttempts == 0)
            {
                message = "Task is already inactive, cannot continue";
                return GameTaskStatus.NotDone;
            }

            if (userGameTask.GameTask.ValidFrom != DateTime.MinValue &&
                userGameTask.GameTask.ValidThru != DateTime.MinValue)
            {
                if (userGameTask.ArrivalTime >= userGameTask.GameTask.ValidFrom.Date && userGameTask.ArrivalTime <= userGameTask.GameTask.ValidThru.Date)
                {
                    message = "You showed up at good time.";
                }
                else
                {
                    message = "You showed up at wrong time.";
                    return GameTaskStatus.NotDone;
                }
            }

            if (userGameTask.GameTask.Type == TaskType.TextEntry)
            {
                if (userGameTask.GameTask.IsActive && userGameTask.TextCriterion.ToLower()
                    .Equals(userGameTask.GameTask.TextCriterion.ToLower()))
                {
                    message = "Task completed, text matched. ";
                }
                else
                {
                    message = "You have entered an incorrect solution for the task";
                    return GameTaskStatus.NotDone;
                }
            }

            if (userGameTask.GameTask.Type is TaskType.CheckInToALocation or TaskType.Event)
            {
                if (userGameTask.GameTask.Place == null)
                {
                    message = "You are outside the location of the selected task";
                    return GameTaskStatus.NotDone;
                }
                var range = SearchNearbyPlaces.DistanceBetweenPlaces(userGameTask.User.Latitude,
                    userGameTask.User.Longitude,
                    userGameTask.GameTask.Place.Latitude, userGameTask.GameTask.Place.Longitude);

                if (range >= 100)
                {
                    message = "You are outside the location of the selected task";
                    return GameTaskStatus.NotDone;
                }

            }

            if (userGameTask.GameTask.Type is TaskType.TakeAPhoto)
            {
                //TODO: Wysyłanie zdjęcia z wiadomością
                //SetUserTaskAsDone(userGameTask);
                _userGameTaskRepository.Update(userGameTask);
                message = "Your submission has been sent to the task author for review, stay tuned!";
                return GameTaskStatus.InReview;

            }

            SetUserTaskAsDone(userGameTask);
            _userGameTaskRepository.Update(userGameTask);
            message += "The task has been completed successfully and the points have been added. Congratulations!";
            return GameTaskStatus.Done;
        }

        public UserGameTask GetUserGameTaskByIds(int userId, int gameTaskId)
        {
            var usersGameTasks = _userGameTaskRepository.GetAllQueryable();
            return usersGameTasks.Include(x => x.GameTask.Place).SingleOrDefault(e => e.UserId == userId && e.GameTaskId == gameTaskId);
        }

        public void AddBonusPoints(int userId, int taskId, int bonusPoints)
        {
            var userGameTask = GetUserGameTaskByIds(userId, taskId);
            //userGameTask.BonusPoints = bonusPoints;
            userGameTask.User.Points += bonusPoints;

            _userGameTaskRepository.Update(userGameTask);
        }

        private void SetUserTaskAsDone(UserGameTask userGameTask)
        {
            userGameTask.User.Points += userGameTask.GameTask.Points;
            userGameTask.IsDone = true;
            userGameTask.DoneOn = DateTime.Now;

            if (userGameTask.GameTask.IsDoneLimited)
            {
                userGameTask.GameTask.LeftDoneAttempts--;
                if (userGameTask.GameTask.LeftDoneAttempts == 0)
                {
                    userGameTask.GameTask.IsActive = false;
                }
            }
            _userGameTaskRepository.Update(userGameTask);
        }
    }
}