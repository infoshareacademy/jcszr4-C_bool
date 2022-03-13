using System.Linq;
using C_Bool.API.DAL.Entities;
using C_bool.API.Repositories;

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

    }
}