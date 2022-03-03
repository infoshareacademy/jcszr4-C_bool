using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using C_Bool.API.DAL.Entities;
using C_bool.API.Repositories;
using MoreLinq;

namespace C_Bool.API.Services
{
    public class ApiReportingPlaceService : IApiReportingPlaceService
    {
        private readonly IRepository<Place> _placeRepository;

        public ApiReportingPlaceService(IRepository<Place> placeRepository)
        {
            _placeRepository = placeRepository;
        }

        public IEnumerable<Place> GetPlaces()
        {
            return _placeRepository.GetAll();
        }

        public void Add(Place place)
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
                .CountBy(x => x.Address)
                .Take(seats)
                .Select(x => x.Key);

            return mostCommonAddress;
        }
    }
}
