using System.Collections.Generic;
using System.Linq;
using C_Bool.API.DAL.Entities;
using C_bool.API.Repositories;
using MoreLinq.Extensions;

namespace C_Bool.API.Services
{
    public class GameTaskReportService : IGameTaskReportService
    {
        private readonly IRepository<GameTaskReport> _gameTaskReportRepository;

        public GameTaskReportService(IRepository<GameTaskReport> gameTaskReportRepository)
        {
            _gameTaskReportRepository = gameTaskReportRepository;
        }
        public void CreateReportEntry(GameTaskReport gameTaskReport)
        {
            _gameTaskReportRepository.Add(gameTaskReport);
        }

        public void UpdateReportEntry(GameTaskReport gameTaskReport)
        {
            _gameTaskReportRepository.Update(gameTaskReport);
        }

        public GameTaskReport GetReportEntryByGameTaskId(int gameTaskId)
        {
            return _gameTaskReportRepository
                .GetAllQueryable()
                .SingleOrDefault(e => e.GameTaskId == gameTaskId);
        }
        // TODO: Dodać zakres czasowy do raportów
        public string TheMostPopularTypeOfTask()
        {
            var grouped = _gameTaskReportRepository
                .GetAll()
                .ToLookup(x => x);

            var maxRepetitions = grouped
                .Max(x => x.Count());

            var maxRepeatedItems = grouped
                .Where(x => x.Count() == maxRepetitions)
                .Select(x => x.Key)
                .Select(x => x.Type);

            var maxRepeatedItem = maxRepeatedItems
                .GroupBy(x => x)
                .MaxBy(x => x.Count())
                .First().Key;

            return maxRepeatedItem.ToString();
        }

        public IEnumerable<KeyValuePair<int, int>> TopListPlacesWithTheMostTask(int x)
        {
            var topListPlacesWithTheMostTask = _gameTaskReportRepository.GetAllQueryable()
                .Where(x => x.GameTaskId > 0).AsEnumerable()
                .CountBy(x => x.PlaceId)
                .OrderByDescending(x => x.Value)
                .Take(x).ToList();

            return topListPlacesWithTheMostTask;
        }
    }
}