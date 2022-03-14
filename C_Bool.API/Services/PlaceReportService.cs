using System;
using System.Collections.Generic;
using System.Linq;
using C_Bool.API.DAL.Entities;
using C_bool.API.Repositories;
using MoreLinq.Extensions;

namespace C_Bool.API.Services
{
    public class PlaceReportService : IPlaceReportService
    {
        private readonly IRepository<PlaceReport> _placeReportRepository;

        public PlaceReportService(IRepository<PlaceReport> placeReportRepository)
        {
            _placeReportRepository = placeReportRepository;
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

        public IEnumerable<string> TopListPlaces(int x)
        {
            var mostCommonAddress = _placeReportRepository.GetAll()
                .GroupBy(x => x)
                .Select(x => x.Key)
                .OrderBy(x => x.Address)
                .CountBy(x => x.Address)
                .Take(x)
                .Select(x => x.Key);

            return mostCommonAddress;
        }
    }
}