using System.Collections.Generic;
using C_Bool.API.DAL.Entities;

namespace C_Bool.API.Services
{
    public interface IGameTaskReportService
    {
        public string TheMostPopularTypeOfTask();
        public IEnumerable<KeyValuePair<int, int>> TopListPlacesWithTheMostTask(int x);
        void CreateReportEntry(GameTaskReport gameTaskReport);
        void UpdateReportEntry(GameTaskReport gameTaskReport);
        GameTaskReport GetReportEntryByGameTaskId(int gameTaskId);
    }
}