using System.Linq;
using C_Bool.API.DAL.Entities;
using C_bool.API.Repositories;

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
    }
}