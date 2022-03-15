using System.Linq;
using C_Bool.API.DAL.Entities;
using C_bool.API.Repositories;

namespace C_Bool.API.Services
{
    public class UserGameTaskReportService : IUserGameTaskReportService
    {
        private readonly IRepository<UserGameTaskReport> _userGameTaskReportRepository;

        public UserGameTaskReportService(IRepository<UserGameTaskReport> userGameTaskReportRepository)
        {
            _userGameTaskReportRepository = userGameTaskReportRepository;
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
    }
}
