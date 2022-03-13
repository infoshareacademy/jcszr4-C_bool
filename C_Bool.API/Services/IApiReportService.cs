using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using C_Bool.API.DAL.Entities;
using C_Bool.API.Enums;
using MoreLinq;

namespace C_Bool.API.Services
{
    public interface IApiReportService
    {
        //PLACE
        public IEnumerable<PlaceReport> GetPlaces();
        public void AddPlace(PlaceReport placeReport);
        public PlaceReport GetPlace(int id);
        public IEnumerable<string> TopListPlaces(int seats);

        //USER
        public IEnumerable<UserReport> GetUser();
        public void AddUser(UserReport userReport);
        public int NumberOfActiveUsers();

        //GAME TASK
        public IEnumerable<GameTaskReport> GetGameTasks();
        public void AddGameTask(GameTaskReport gameTaskReport);
        public string TheMostPopularTypeOfTask();
        public IEnumerable<KeyValuePair<int, int>> TopListPlacesWithTheMostTask(int seats);
        public IEnumerable<int> Proba(int seats);

    }
}
