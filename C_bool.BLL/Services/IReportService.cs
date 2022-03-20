using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using C_bool.BLL.DAL.Entities;
using C_bool.BLL.DTOs;
using C_bool.BLL.Models.Reports;

namespace C_bool.BLL.Services
{
    public interface IReportService
    {
        Task CreateUserReportEntry(User user);
        Task UpdateUserReportEntry(User user);
        Task CreatePlaceReportEntry(Place place);
        Task UpdatePlaceReportEntry(Place place);
        Task CreateGameTaskReportEntry(GameTask gameTask);
        Task UpdateGameTaskReportEntry(GameTask gameTask); 
        Task CreateUserGameTaskReportEntry(UserGameTask userGameTask);
        Task UpdateUserGameTaskReportEntry(UserGameTask userGameTask);
        Task<ActiveUsersCount> GetActiveUsersCount();
        Task<GameTaskPointsAverage> GetGameTaskPointsAverage();
        Task<List<GameTaskTypeClassification>> GetGameTaskTypeClassification(DateTime? dateFrom, DateTime? dateTo, int limit);
        Task<List<PlaceByGameTasksClassification>> GetPlaceByGameTasksClassification(DateTime? dateFrom, DateTime? dateTo, int limit);
        Task<PlaceWithoutGameTasksCount> GetPlacesWithoutGameTasksCount();
        Task<List<UserGameTaskByUsersClassification>> GetUserGameTaskByUsersClassification(DateTime? dateFrom, DateTime? dateTo, int limit);
        Task<List<UserGameTaskMostActiveUsersClassification>> GetUserGameTaskMostActiveUsersClassification(DateTime? dateFrom, DateTime? dateTo, int limit);
        Task<UserGameTaskDoneTimeAverage> GetUserGameTaskDoneTimeAverage();
        Task<UserGameTaskDoneCount> GetUserGameTaskDoneCount();
    }
}