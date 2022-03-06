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
    public class ApiReportService : IApiReportService
    {
        private readonly IRepository<Place> _placeRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<GameTask> _gameTaskRepository;

        public ApiReportService(
            IRepository<Place> placeRepository, 
            IRepository<User> useRepository, 
            IRepository<GameTask> gameTaskRepository
        )
        {
            _placeRepository = placeRepository;
            _userRepository = useRepository;
            _gameTaskRepository = gameTaskRepository;
        }
        // TODO: Dodać zakres czasowy do raportów
        // PLACE 
        public IEnumerable<Place> GetPlaces()
        {
            return _placeRepository.GetAll();
        }

        public void AddPlace(Place place)
        {
            _placeRepository.Add(place);
        }

        public Place GetPlace(int id)
        {
            return _placeRepository.GetById(id);
        }

        public IEnumerable<string> TopListPlaces(int seats)
        {
            var mostCommonAddress = _placeRepository.GetAll()
                .GroupBy(x => x)
                .Select(x => x.Key)
                .OrderBy(x => x.Address)
                .CountBy(x=>x.Address)
                .Take(seats)
                .Select(x=>x.Key);

            return mostCommonAddress;
        }

        //USER
        public IEnumerable<User> GetUser()
        {
            return _userRepository.GetAll();
        }

        public void AddUser(User user)
        {
            _userRepository.Add(user);
        }

        public int NumberOfActiveUsers()
        {
            var numberOfActiveUsers = _userRepository.GetAll().Count(x => x.IsActive==true);
            return numberOfActiveUsers;
        }

        //GAME TASK
        public IEnumerable<GameTask> GetGameTasks()
        {
            return _gameTaskRepository.GetAll();
        }

        public void AddGameTask(GameTask gameTask)
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

        //UserGameTaskReport


        public IEnumerable<int> Proba(int seats)
        {
            throw new NotImplementedException();
        }
    }
}
