using System.Collections.Generic;
using C_Bool.API.DAL.Entities;

namespace C_Bool.API.Services
{
    public interface IPlaceReportService
    {
        void CreateReportEntry(PlaceReport placeReport);
        void UpdateReportEntry(PlaceReport placeReport);
        PlaceReport GetReportEntryByPlaceId(int placeId);
        public IEnumerable<string> TopListPlaces(int x);
    }
}