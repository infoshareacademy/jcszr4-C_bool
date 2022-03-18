using System;
using System.Collections.Generic;
using System.Linq;
using C_Bool.API.DAL.Entities;
using C_Bool.API.DTOs;
using C_bool.API.Repositories;
using MoreLinq;

namespace C_Bool.API.Services
{
    public class UserGameTaskReportService : IUserGameTaskReportService
    {
        private readonly IRepository<UserGameTaskReport> _userGameTaskReportRepository;
        private readonly IRepository<GameTaskReport> _gameTaskReportRepository;
        private readonly IRepository<UserReport> _userReportRepository;

        public UserGameTaskReportService(IRepository<UserGameTaskReport> userGameTaskReportRepository, IRepository<GameTaskReport> gameTaskReportRepository, IRepository<UserReport> userReportRepository)
        {
            _userGameTaskReportRepository = userGameTaskReportRepository;
            _gameTaskReportRepository = gameTaskReportRepository;
            _userReportRepository = userReportRepository;
        }

        public void CreateReportEntry(UserGameTaskReport userGameTaskReport)
        {
            
            _userGameTaskReportRepository.Add(userGameTaskReport);
        }

        public void UpdateReportEntry(UserGameTaskReport userGameTaskReport)
        {
            _userGameTaskReportRepository.Update(userGameTaskReport);
        }

        public UserGameTaskReport GetReportEntryByUserGameTaskId(int userGameTaskId)
        {
            return _userGameTaskReportRepository
                .GetAllQueryable()
                .SingleOrDefault(e => e.UserGameTaskId == userGameTaskId);
        }

        public List<GetCountByDto> GetMostPopularByUsers(DateTime? dateFrom, DateTime? dateTo, int limit)
        {
            if (!_userGameTaskReportRepository.GetAllQueryable().Any())
            {
                return new List<GetCountByDto>();
            }
            dateFrom ??= _userGameTaskReportRepository.GetAllQueryable().Min(e => e.CreatedOn);
            dateTo ??= _userGameTaskReportRepository.GetAllQueryable().Max(e => e.CreatedOn);
            limit = (limit < 1) ? 10 : limit;
            var gameTaskCountByUsers = _userGameTaskReportRepository
                .GetAllQueryable()
                .Where(e => e.CreatedOn >= dateFrom && e.CreatedOn <= dateTo)
                .AsEnumerable()
                .CountBy(e => e.UserId)
                .Select(e => new
                {
                    GameTaskId = e.Key,
                    Count = e.Value
                })
                .ToList();
            var mostPopularByUser = gameTaskCountByUsers
                .Join(
                    _gameTaskReportRepository
                        .GetAllQueryable()
                        .Select(e => new
                        {
                            e.GameTaskId,
                            e.GameTaskName
                        }),
                    ugt => ugt.GameTaskId,
                    gt => gt.GameTaskId,
                    (ugt, gt) =>
                        new GetCountByDto()
                        {
                            Name = gt.GameTaskName,
                            Count = ugt.Count
                        })
                .OrderByDescending(e => e.Count)
                .ThenBy(e => e.Name)
                .Take(limit)
                .ToList();

            return mostPopularByUser;
        }

        public List<GetCountByDto> GetMostActiveUsers(DateTime? dateFrom, DateTime? dateTo, int limit)
        {
            if (!_userGameTaskReportRepository.GetAllQueryable().Any())
            {
                return new List<GetCountByDto>();
            }
            dateFrom ??= _userGameTaskReportRepository.GetAllQueryable().Min(e => e.CreatedOn);
            dateTo ??= _userGameTaskReportRepository.GetAllQueryable().Max(e => e.CreatedOn);
            limit = (limit < 1) ? 10 : limit;
            var gameTaskCountByUsers = _userGameTaskReportRepository
                .GetAllQueryable()
                .Where(e => e.CreatedOn >= dateFrom && e.CreatedOn <= dateTo)
                .Where(e => e.IsDone)
                .AsEnumerable()
                .CountBy(e => e.UserId)
                .Select(e => new
                {
                    UserId = e.Key,
                    Count = e.Value
                })
                .ToList();
            var mostActiveUsers = gameTaskCountByUsers
                .Join(
                    _userReportRepository
                        .GetAllQueryable()
                        .Select(e => new
                        {
                            e.UserId,
                            e.UserName
                        }),
                    ugt => ugt.UserId,
                    gt => gt.UserId,
                    (ugt, gt) =>
                        new GetCountByDto()
                        {
                            Name = gt.UserName,
                            Count = ugt.Count
                        })
                .OrderByDescending(e => e.Count)
                .ThenBy(e => e.Name)
                .Take(limit)
                .ToList();

            return mostActiveUsers;
        }

        public GetPartialCountDto GetDoneGameTaskCount()
        {
            var doneUserGameTaskCount = _userGameTaskReportRepository
                .GetAllQueryable()
                .Count(e => e.IsDone);

            var totalUserGameTask = _userGameTaskReportRepository.GetAllQueryable().Count();

            var doneGameTaskCount = new GetPartialCountDto
            {
                PartialCount = doneUserGameTaskCount,
                TotalCount = totalUserGameTask
            };

            return doneGameTaskCount;
        }

        public GetAverageDto GetAverageTimeBetweenCreatingAndDone()
        {
            if (!_userGameTaskReportRepository.GetAllQueryable().Any())
            {
                return new GetAverageDto();
            }
            var averageTime = _userGameTaskReportRepository
                .GetAllQueryable()
                .Where(e => e.IsDone)
                .Select(e => new
                {
                    TimeSpan = e.DoneOn - e.CreatedOn
                })
                .AsEnumerable()
                .Average(e => e.TimeSpan.TotalMilliseconds);

            return new GetAverageDto
            {
                Average = averageTime
            };
        }
    }
}
