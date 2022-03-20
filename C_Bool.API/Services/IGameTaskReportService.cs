using System;
using System.Collections.Generic;
using C_Bool.API.DAL.Entities;
using C_Bool.API.DTOs;

namespace C_Bool.API.Services
{
    public interface IGameTaskReportService
    {
        void CreateReportEntry(GameTaskReport gameTaskReport);
        void UpdateReportEntry(GameTaskReport gameTaskReport);
        GameTaskReport GetReportEntryByGameTaskId(int gameTaskId);
        List<GetCountByDto> GetCountByType(DateTime? dateFrom, DateTime? dateTo);
        GetAverageDto GetPointsAverage(DateTime? dateFrom, DateTime? dateTo);
    }
}