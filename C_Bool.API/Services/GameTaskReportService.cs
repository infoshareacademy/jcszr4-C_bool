using System;
using System.Collections.Generic;
using System.Linq;
using C_Bool.API.DAL.Entities;
using C_Bool.API.DTOs;
using C_bool.API.Repositories;
using MoreLinq.Extensions;

namespace C_Bool.API.Services
{
    public class GameTaskReportService : IGameTaskReportService
    {
        private readonly IRepository<GameTaskReport> _gameTaskReportRepository;

        public GameTaskReportService(IRepository<GameTaskReport> gameTaskReportRepository)
        {
            _gameTaskReportRepository = gameTaskReportRepository;
        }
        public void CreateReportEntry(GameTaskReport gameTaskReport)
        {
            _gameTaskReportRepository.Add(gameTaskReport);
        }

        public void UpdateReportEntry(GameTaskReport gameTaskReport)
        {
            _gameTaskReportRepository.Update(gameTaskReport);
        }

        public GameTaskReport GetReportEntryByGameTaskId(int gameTaskId)
        {
            return _gameTaskReportRepository
                .GetAllQueryable()
                .SingleOrDefault(e => e.GameTaskId == gameTaskId);
        }

        public List<GetCountByDto> GetCountByType(DateTime? dateFrom, DateTime? dateTo)
        {
            if (!_gameTaskReportRepository.GetAllQueryable().Any())
            {
                return new List<GetCountByDto>();
            }
            dateFrom ??= _gameTaskReportRepository.GetAllQueryable().Min(e => e.CreatedOn);
            dateTo ??= _gameTaskReportRepository.GetAllQueryable().Max(e => e.CreatedOn);
            var gameTaskTypeList = _gameTaskReportRepository
                .GetAllQueryable()
                .Where(e => e.CreatedOn >= dateFrom && e.CreatedOn <= dateTo)
                .Select(e => new
                {
                    TypeName = e.Type
                })
                .ToList();
            var gameTaskReportCountByType = gameTaskTypeList
                .CountBy(e => e.TypeName.ToString())
                .Select(kv => new GetCountByDto
                {
                    Name = kv.Key,
                    Count = kv.Value
                })
                .OrderByDescending(e => e.Count)
                .ThenBy(e => e.Name)
                .ToList();
            return gameTaskReportCountByType;
        }

        public GetAverageDto GetPointsAverage(DateTime? dateFrom, DateTime? dateTo)
        {
            if (!_gameTaskReportRepository.GetAllQueryable().Any())
            {
                return new GetAverageDto();
            }
            dateFrom ??= _gameTaskReportRepository.GetAllQueryable().Min(e => e.CreatedOn);
            dateTo ??= _gameTaskReportRepository.GetAllQueryable().Max(e => e.CreatedOn);

            var pointsAverage = _gameTaskReportRepository
                .GetAllQueryable()
                .Where(e => e.CreatedOn >= dateFrom && e.CreatedOn <= dateTo)
                .Average(e => e.Points);

            var getAverageDto = new GetAverageDto
            {
                Average = Math.Round(pointsAverage)
            };
            return getAverageDto;
        }
    }
}