using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using C_Bool.API.DAL.Entities;
using C_Bool.API.Enums;
using MoreLinq;

namespace C_Bool.API.Services
{
    public interface IApReportingGameTaskService
    {
        //GAME TASK
        public IEnumerable<GameTask> GetGameTasks();
        public void Add(GameTask gameTask);
        public string TheMostPopularTypeOfTask();
        public IEnumerable<KeyValuePair<int, int>> TopListPlacesWithTheMostTask(int seats);
        public List<KeyValuePair<int, int>> Test(int x);

    }
}
