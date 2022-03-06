using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using MoreLinq;
using System.Threading.Tasks;
using C_Bool.API.DAL.Entities;
using C_Bool.API.Enums;
using C_bool.API.Repositories;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace C_Bool.API.Services
{
    public class ApiReportingGameTaskService : IApReportingGameTaskService
    {
        private readonly IRepository<Place> _placeRepository;
        private readonly IRepository<GameTask> _gameTaskRepository;

        public ApiReportingGameTaskService(IRepository<Place> placeRepository, IRepository<GameTask> gameTaskRepository)
        {
            _placeRepository = placeRepository;
            _gameTaskRepository = gameTaskRepository;
        }
        // TODO: Dodać zakres czasowy do raportów
        // PLACE 

        //GAME TASK
        public IEnumerable<GameTask> GetGameTasks()
        {
            return _gameTaskRepository.GetAll();
        }

        public void Add(GameTask gameTask)
        {
            _gameTaskRepository.Add(gameTask);
        }

        public string TheMostPopularTypeOfTask()
        {
            var grouped = _gameTaskRepository
                .GetAll()
                .ToLookup(x => x);

            var maxRepetitions = grouped
                .Max(x => x.Count());

            var maxRepeatedItems = grouped
                .Where(x => x.Count() == maxRepetitions)
                .Select(x => x.Key)
                .Select(x=>x.Type);

            var maxRepeatedItem = maxRepeatedItems
                .GroupBy(x => x)
                .MaxBy(x => x.Count())
                .First().Key;

            return maxRepeatedItem.ToString();
        }

        public IEnumerable<KeyValuePair<int, int>> TopListPlacesWithTheMostTask(int seats)
        {
            var topListPlacesWithTheMostTask = _gameTaskRepository.GetAllQueryable()
                .GroupBy(x => x)
                .Select(x => x.Key).Where(x=>x.GameTaskId>0).AsEnumerable()
                .CountBy(x => x.PlaceId)
                .OrderByDescending(x=>x.Value)
                .Take(seats).ToList();

            return topListPlacesWithTheMostTask;
        }

        public List<KeyValuePair<int, int>> Test(int x)
        {
            var test1 = _gameTaskRepository.GetAll()
                .Join(_placeRepository.GetAll()
                    , g => g.PlaceId
                    , p => p.PlaceId
                    ,(g, p) => new
                        {
                            gameTaskPlaceId=g.PlaceId
                            ,placeeId = p.PlaceId
                        });
            var test2 = test1
                .GroupBy(x => x)
                .Select(x => x.Key)
                .CountBy(x => x.placeeId)
                .OrderByDescending(x => x.Value)
                .Take(x).ToList();
            return test2;
        }
    }
}
