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
        public IEnumerable<Place> GetPlaces();
        public void Add(Place place);
        public Place GetPlace(int id);
        public IEnumerable<string> TopListPlaces(int seats);

        //USER
        public IEnumerable<User> GetUser();
        public void Add(User user);
        public int NumberOfActiveUsers();

        //GAME TASK
        public IEnumerable<GameTask> GetGameTasks();
        public void Add(GameTask gameTask);
        public string TheMostPopularTypeOfTask();
        public IEnumerable<KeyValuePair<int, int>> TopListPlacesWithTheMostTask(int seats);
        public IEnumerable<int> Proba(int seats);

    }
}
