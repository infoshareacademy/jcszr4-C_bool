using System.Linq;
using C_Bool.API.DAL.Entities;
using C_Bool.API.DTOs;
using C_bool.API.Repositories;

namespace C_Bool.API.Services
{
    public class UserReportService : IUserReportService
    {
        private readonly IRepository<UserReport> _userReportRepository;

        public UserReportService(IRepository<UserReport> userReportRepository)
        {
            _userReportRepository = userReportRepository;
        }

        public void CreateReportEntry(UserReport userReport)
        {
            _userReportRepository.Add(userReport);
        }

        public void UpdateReportEntry(UserReport userReport)
        {
            _userReportRepository.Update(userReport);
        }

        public UserReport GetReportEntryByUserId(int userId)
        {
            return _userReportRepository
                .GetAllQueryable()
                .SingleOrDefault(e => e.UserId == userId);
        }

        public GetPartialCountDto ActiveUsersCount()
        {
            var activeUsersCount = _userReportRepository
                .GetAllQueryable()
                .Count(x => x.IsActive);

            var totalUsersCount = _userReportRepository
                .GetAllQueryable()
                .Count();
            return new GetPartialCountDto
            {
                PartialCount = activeUsersCount,
                TotalCount = totalUsersCount
            };
        }
    }
}