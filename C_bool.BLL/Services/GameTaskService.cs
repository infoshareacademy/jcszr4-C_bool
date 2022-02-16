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

        public void ManuallyCompleteTask(int taskId, int userId)
        {
            //jak text entry - tylko manualnie
            //pojawienie się informacji dla użytkownika, że pojawiło mu się zadanie do zatwierdzenia. (stworzyć klasę)
            //dodajemy do UserGameTask - zdjecie stringa

            //[AP] ponizej pobieramy full outer joina 3 tabel User i GameTask i UserGameTask takze mamy dostep do wszystkich pol i mozemy je dowolnie updatowac  
            var userGameTask = GetUserGameTaskByIds(userId, taskId);

            //[AP] tutaj dodalem 2 nowe propercje - jedna to flaga czy liczba wykonan ma byc okreslona, a druga to liczba wykonan, ktora zostala
            if (!userGameTask.GameTask.IsDoneLimited || userGameTask.GameTask.LeftDoneAttempts > 0)
            {
                //[AP] tu sprawdzamy czy nas game task wziety z naszej zjoinowanej tabeli zgada sie z zalozonym typem zadania
                if (userGameTask.GameTask.Type == TaskType.TakeAPhoto)
                {
                    //[AP] tu sprawdzamy czy zadanie jest aktywne - noramlnie mogloby to pojsc do tego ifa wyzej, ale chyba bedziemy chcieli kiedys dodac
                    //[AP] cos to poinformuje uzytkownika, ze zadanie jest nieaktywne 
                    if (userGameTask.GameTask.IsActive)
                    {
                        //[AP] tu ustawiamy wszystkie pola ktore sie zmieniaja w zwiazku z pomyslnym zakonczeniem zadania
                        SetUserTaskAsDone(userGameTask);
                    }
                    //[AP] tu na koniec update bazy poprzez dbcontext, ktore pobieramy z repo
                    _userGameTaskRepository.Update(userGameTask);
                }
            }
        }

        public bool CompleteTask(int taskId, int userId, out string message)
        {
            var userGameTask = GetUserGameTaskByIds(userId, taskId);
            message = string.Empty;

            if (!userGameTask.GameTask.IsActive)
            {
                message = "Task is already inactive, cannot continue";
                return false;
            }

            if (userGameTask.GameTask.IsDoneLimited && userGameTask.GameTask.LeftDoneAttempts == 0)
            {
                message = "Task is already inactive, cannot continue";
                return false;
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
                    return false;
                }
            }

            if (userGameTask.GameTask.Type == TaskType.TextEntry)
            {
                if (userGameTask.GameTask.IsActive && userGameTask.TextCriterion.ToLower()
                    .Equals(userGameTask.GameTask.TextCriterion.ToLower()))
                {
                    message = "Task completed, text matched.";
                }
                else
                {
                    message = "You have entered an incorrect solution for the task";
                    return false;
                }
            }

            if (userGameTask.GameTask.Type is TaskType.CheckInToALocation or TaskType.Event)
            {
                if (userGameTask.GameTask.Place == null)
                {
                    message = "You are outside the location of the selected task";
                    return false;
                }
                var range = SearchNearbyPlaces.DistanceBetweenPlaces(userGameTask.User.Latitude,
                    userGameTask.User.Longitude,
                    userGameTask.GameTask.Place.Latitude, userGameTask.GameTask.Place.Longitude);

                if (range >= 100)
                {
                    message = "You are outside the location of the selected task";
                    return false;
                }

            }

            SetUserTaskAsDone(userGameTask);
            _userGameTaskRepository.Update(userGameTask);
            message += "The task has been completed successfully and the points have been added. Congratulations!";
            return true;
        }

        //[AP] tu zrobilem taka metode do wyciagania konktretnego UserGameTaska z bazy - potem wystarczy kliknac .User albo .Task i mamy caly obiekt danego typu
        //[AP] co moze byc przydatene w kontrolerach
        public UserGameTask GetUserGameTaskByIds(int userId, int gameTaskId)
        {
            var usersGameTasks = _userGameTaskRepository.GetAllQueryable();
            return usersGameTasks.Include(x => x.GameTask.Place).SingleOrDefault(e => e.UserId == userId && e.GameTaskId == gameTaskId);
        }

        //[AP] dodalem jeszcze metode do ewentualnego dodawania punktow bonusowych uzytkownikowi do konkretnego zadania 
        public void AddBonusPoints(int userId, int taskId, int bonusPoints)
        {
            var userGameTask = GetUserGameTaskByIds(userId, taskId);
            //userGameTask.BonusPoints = bonusPoints;
            userGameTask.User.Points += bonusPoints;

            _userGameTaskRepository.Update(userGameTask);
        }

        //[AP] ta metode zabralem z UserService, troche przerobilem i wrzucam ja jednak tutaj bo jest bardziej pozyteczna tutaj - nie powinna byc uzywana nigdzie indziej w aplikacji
        private void SetUserTaskAsDone(UserGameTask userGameTask)
        {
            userGameTask.User.Points += userGameTask.GameTask.Points;
            userGameTask.IsDone = true;
            userGameTask.DoneOn = DateTime.Now;

            //[AP] jesli flaga isDoneLimited to odejmujemy 1 bo ktos wlasnie zrobil to zadanie
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