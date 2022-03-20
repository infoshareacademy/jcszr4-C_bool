using System;
using System.Collections.Generic;
using System.Linq;
using C_Bool.API.DAL.Entities;
using C_Bool.API.DTOs;
using C_bool.API.Repositories;
using MoreLinq.Extensions;

namespace C_Bool.API.Services
{
    public class PlaceReportService : IPlaceReportService
    {
        private readonly IRepository<PlaceReport> _placeReportRepository;
        private readonly IRepository<GameTaskReport> _gameTaskReportRepository;

        public PlaceReportService(IRepository<PlaceReport> placeReportRepository, IRepository<GameTaskReport> gameTaskReportRepository)
        {
            _placeReportRepository = placeReportRepository;
            _gameTaskReportRepository = gameTaskReportRepository;
        }
        public void CreateReportEntry(PlaceReport placeReport)
        {
            _placeReportRepository.Add(placeReport);
        }

        public void UpdateReportEntry(PlaceReport placeReport)
        {
            _placeReportRepository.Update(placeReport);
        }

        public PlaceReport GetReportEntryByPlaceId(int placeId)
        {
            return _placeReportRepository
                .GetAllQueryable()
                .SingleOrDefault(e => e.PlaceId == placeId);
        }

        public List<GetCountByDto> GetMostPopularByGameTask(DateTime? dateFrom, DateTime? dateTo, int limit)
        {
            if (!_gameTaskReportRepository.GetAllQueryable().Any())
            {
                return new List<GetCountByDto>();
            }
            dateFrom ??= _gameTaskReportRepository.GetAllQueryable().Min(e => e.CreatedOn);
            dateTo ??= _gameTaskReportRepository.GetAllQueryable().Max(e => e.CreatedOn);
            limit = limit < 1 ? 10 : limit;
            var gameTaskCountByPlaces = _gameTaskReportRepository.GetAllQueryable()
                .Where(e => e.CreatedOn >= dateFrom && e.CreatedOn <= dateTo)
                .AsEnumerable()
                .CountBy(e => e.PlaceId)
                .Select(e => new
                {
                    PlaceId = e.Key,
                    Count = e.Value
                })
                .ToList();
            var placesWithHighestGameTaskCount = gameTaskCountByPlaces
                .Join(
                    _placeReportRepository
                        .GetAllQueryable()
                        .Select(e => new
                        {
                            e.PlaceId,
                            e.PlaceName
                        }),
                    gt => gt.PlaceId,
                    p => p.PlaceId,
                    (p, gt) =>
                        new GetCountByDto
                        {
                            Name = gt.PlaceName,
                            Count = p.Count
                        })
                .OrderByDescending(e => e.Count)
                .ThenBy(e => e.Name)
                .Take(limit)
                .ToList();

            return placesWithHighestGameTaskCount;
        }

        public GetPartialCountDto GetWithoutGameTask()
        {
            var totalPlaceCount = _placeReportRepository
                .GetAllQueryable()
                .Count();
            var placeWithGameTaskCount = _gameTaskReportRepository
                .GetAll()
                .DistinctBy(e => e.PlaceId)
                .Count();
            var partialCountDto = new GetPartialCountDto
            {
                PartialCount = totalPlaceCount - placeWithGameTaskCount,
                TotalCount = totalPlaceCount
            };

            return partialCountDto;
        }

        //public IEnumerable<string> TopListPlaces(int x)
        //{
        //    var mostCommonAddress = _placeReportRepository.GetAll()
        //        .GroupBy(x => x)
        //        .Select(x => x.Key)
        //        .OrderBy(x => x.Address)
        //        .CountBy(x => x.Address)
        //        .Take(x)
        //        .Select(x => x.Key);

        //    return mostCommonAddress;
        //}
    }
}