using System;
using System.Collections.Generic;
using C_Bool.API.DAL.Entities;
using C_Bool.API.DTOs;

namespace C_Bool.API.Services
{
    public interface IUserGameTaskReportService
    {
        void CreateReportEntry(UserGameTaskReport userGameTaskReport);
        void UpdateReportEntry(UserGameTaskReport userGameTaskReport);
        UserGameTaskReport GetReportEntryByUserGameTaskId(int userGameTaskId);
        List<GetCountByDto> GetMostPopularByUsers(DateTime? dateFrom, DateTime? dateTo, int limit);
        List<GetCountByDto> GetMostActiveUsers(DateTime? dateFrom, DateTime? dateTo, int limit);
        GetPartialCountDto GetDoneGameTaskCount();
        GetAverageDto GetAverageTimeBetweenCreatingAndDone();
    }
}