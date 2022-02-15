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
        public void CompleteTask(int taskId, int userId)
        {
            // nie ma ram czasowych i nie wymaga lokalizacji - tylko okazania dowodu w postaci tekstu
            // zaliczenie automatyczne - taki sam (tylko z malych liter albo duzych albo bez polskich znakow)
            
            //[AP] tu w zasadzie ta sama sytuacja co powyzej czyli robimy joina 3 tabel i wszystko leci tak samo jak w metodzie wyzej
            var userGameTask = GetUserGameTaskByIds(userId, taskId);
            
            if (!userGameTask.GameTask.IsDoneLimited || userGameTask.GameTask.LeftDoneAttempts > 0)
            {
                if (userGameTask.GameTask.Type == TaskType.TextEntry)
                {
                    //[AP] tu na razie ustawilem ze musi byc equal - do ewentualnego rozkminienia
                    if (userGameTask.GameTask.IsActive && userGameTask.TextCriterion.ToLower()
                        .Equals(userGameTask.GameTask.TextCriterion.ToLower()))
                    {
                        SetUserTaskAsDone(userGameTask);
                    }
                }

                //TODO:
                //zadanie tego typu ma ustawione ramy czasowe ValidFrom i ValidThru, czyli można to traktować jako wydarzenie
                //np osoba tworzaca zadanie ustawia ze dnia 1 lutego o godzinie 20 -22 jest koncert Zenka, na któym możesz się pojawić
                //tego typu zadania miałyby "kalendarz" na stronie głównej
                if (userGameTask.GameTask.Type == TaskType.Event)
                {
                    if (userGameTask.GameTask.IsActive &&
                        userGameTask.ArrivalTime <= userGameTask.GameTask.ValidFrom.Date &&
                        userGameTask.ArrivalTime >= userGameTask.GameTask.ValidThru.Date)
                    {
                        SetUserTaskAsDone(userGameTask);
                    }
                }

                //to zadanie zakłąda że użytkownik odwiedzi lokalizację i może je zaliczyć tylko jak w niej jest, podobne jak event tylko bez ram czasowych
                if (userGameTask.GameTask.Type == TaskType.CheckInToALocation)
                {
                    //tu wyciagnalem sprawdzanie czy place jest null wyzej bo przy wyznaczaniu range juz potrzebujemy tej informacji
                    // z drugiej strony w przyszlosci moze jakos ogarniemy informacje dla uzytkownika dlatego nie wyciagam tego to glownego ifa
                    if (userGameTask.GameTask.Place != null)
                    {
                        //[AP] tu wiadomo wyciagamy odleglosc miedy miejscem a userem
                        var range = SearchNearbyPlaces.DistanceBetweenPlaces(userGameTask.User.Latitude,
                            userGameTask.User.Longitude,
                            userGameTask.GameTask.Place.Latitude, userGameTask.GameTask.Place.Longitude);

                        //[AP] dodalem nowa property MaxRange do GameTaska zeby dalo sie okreslic jak maksymalnie daleko moze byc user od danego miejsca z zadania
                        // w zaleznosci od zadania moze byc mniej albo wiecej
                        if (userGameTask.GameTask.IsActive && range <= 100) 
                        {
                            SetUserTaskAsDone(userGameTask);
                        }
                    }
                }
                //[AP] no i na koniec update calego userGameTaska w bazie przez dbContext z repo
                _userGameTaskRepository.Update(userGameTask);
            }
        }

        //[AP] tu zrobilem taka metode do wyciagania konktretnego UserGameTaska z bazy - potem wystarczy kliknac .User albo .Task i mamy caly obiekt danego typu
        //[AP] co moze byc przydatene w kontrolerach
        public UserGameTask GetUserGameTaskByIds(int userId, int gameTaskId)
        {
            var usersGameTasks = _userGameTaskRepository.GetAllQueryable();
            return usersGameTasks.SingleOrDefault(e => e.UserId == userId && e.GameTaskId == gameTaskId);
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
            }
        }
    }
}