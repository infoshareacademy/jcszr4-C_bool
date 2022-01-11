using System;
using System.Collections.Generic;
using System.Linq;
using C_bool.BLL.DAL.Entities;
using C_bool.BLL.Enums;
using C_bool.BLL.Repositories;
using Microsoft.IdentityModel.Tokens;

namespace C_bool.WebApp.Services
{
    public class GameTaskService 
    {
        private readonly IRepository<GameTask> _gameTasksRepository;

        public GameTaskService(IRepository<GameTask> gameTasks)
        {
            _gameTasksRepository = gameTasks;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
        public GameTaskService(User user, List<GameTask> userGameTask)
        {
            string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            List<GameTask> userId = (List<GameTask>)userGameTask.Where(u => u.Name == userName);
            Place userPlace = user.FavPlaces;
            List<GameTask> gameTasks = new List<GameTask>
            {
                new()
                {
                    Id = 0,
                    CreatedOn = DateTime.UtcNow,
                    Place =userPlace,
                    Name = userPlace.Name,
                    ShortDescription = "Zadanie za 100 punktów",
                    Description = "Jeżeli jako pierwszy dotrzesz na miejsce, dostaniesz 100 punktów",
                    Type = TaskType.FirstComeFirstServed,
                    Points = 100,
                    ValidFrom = DateTime.Today,
                    ValidThru = DateTime.Today.AddDays(14),
                    IsActive = false,
                    CreatedByName = userName,
                    UserGameTasks = new List<UserGameTask>((IEnumerable<UserGameTask>)userId)
                },
                new()
                {
                    Id = 1,
                    CreatedOn = DateTime.UtcNow,
                    Place =userPlace,
                    Name = userPlace.Name,
                    ShortDescription = "Zadanie za 75 punktów",
                    Description = "Dodaj zdjęcie miejsca, w którym chcesz wykonać zadanie!",
                    Type = TaskType.TakeAPhoto,
                    //Photo = 
                    Points = 75,
                    ValidFrom = DateTime.Today,
                    ValidThru = DateTime.Today.AddDays(14),
                    IsActive = false,
                    CreatedByName = userName,
                    UserGameTasks = new List<UserGameTask>((IEnumerable<UserGameTask>)userId)
                },
                new()
                {
                    Id = 2,
                    CreatedOn = DateTime.UtcNow,
                    Place =userPlace,
                    Name = userPlace.Name,
                    ShortDescription = "Zadanie za 50 punktów",
                    Description = "Zamelduj się w miejscu zadania!!",
                    Type = TaskType.CheckInToALocation,
                    Points = 50,
                    ValidFrom = DateTime.Today,
                    ValidThru = DateTime.Today.AddDays(14),
                    IsActive = false,
                    CreatedByName = userName,
                    UserGameTasks = new List<UserGameTask>((IEnumerable<UserGameTask>)userId)
                },
                new()
                {
                    Id = 3,
                    CreatedOn = DateTime.UtcNow,
                    Place =userPlace,
                    Name = userPlace.Name,
                    ShortDescription = "Zadanie za 85 punktów",
                    Description = "Zamelduj się o godzinie...!!",
                    Type = TaskType.CheckInAtTheSpecifiedTime,
                    Points = 85,
                    ValidFrom = DateTime.Today,
                    ValidThru = DateTime.Today.AddDays(14),
                    IsActive = false,
                    CreatedByName = userName,
                    UserGameTasks = new List<UserGameTask>((IEnumerable<UserGameTask>)userId)
                },
                new()
                {
                    Id = 4,
                    CreatedOn = DateTime.UtcNow,
                    Place =userPlace,
                    Name = userPlace.Name,
                    ShortDescription = "Zadanie za 25 punktów",
                    Description = "Zamelduj się o godzinie...!!",
                    Type = TaskType.TextEntry,
                    Points = 25,
                    ValidFrom = DateTime.Today,
                    ValidThru = DateTime.Today.AddDays(14),
                    IsActive = false,
                    CreatedByName = userName,
                    UserGameTasks = new List<UserGameTask>((IEnumerable<UserGameTask>)userId)
                },
            };
        }


        // Completed task-> metoda Andrzeja GetDoneTasks
        
        public void ManuallyCompleteTask(int taskId, int userId)
        {
            throw new NotImplementedException();
        }


        public bool CompleteTask(int taskId, int userId, string textToConfirm)
        {
            throw new NotImplementedException();
        }

        ///
        public void CompleteTask(int taskId, int userId, string latitude, string longitude)
        {
            var task = _gameTasksRepository.GetById(taskId);
            throw new NotImplementedException();

        }

        public bool CompleteTask(int taskId, int userId, string latitude, string longitude, DateTime timeOfVisit)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Complete task method overload for task type ....
        /// </summary>
        public bool CompleteTask(int taskId, int userId)
        {
            throw new NotImplementedException();
        }
    }
}