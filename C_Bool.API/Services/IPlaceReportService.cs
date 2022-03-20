using System;
using System.Collections.Generic;
using C_Bool.API.DAL.Entities;
using C_Bool.API.DTOs;

namespace C_Bool.API.Services
{
    public interface IPlaceReportService
    {
        void CreateReportEntry(PlaceReport placeReport);
        void UpdateReportEntry(PlaceReport placeReport);
        PlaceReport GetReportEntryByPlaceId(int placeId);
        List<GetCountByDto> GetMostPopularByGameTask(DateTime? dateFrom, DateTime? dateTo, int limit);
        GetPartialCountDto GetWithoutGameTask();
    }
}