using System.Collections.Generic;
using C_Bool.API.DAL.Entities;
using C_bool.API.Repositories;

namespace C_Bool.API.Services
{
    public class UserGameTaskReportService : IUserGameTaskReportService
    {
        private readonly IRepository<UserGameTaskReport> _userGameTaskRepository;

        public UserGameTaskReportService(IRepository<UserGameTaskReport> userGameTaskRepository)
        {
            _userGameTaskRepository = userGameTaskRepository;
        }

        public void AddUserGameTask(UserGameTaskReport userGameTaskReport)
        {
            _userGameTaskRepository.Add(userGameTaskReport);
        }
    }
}
