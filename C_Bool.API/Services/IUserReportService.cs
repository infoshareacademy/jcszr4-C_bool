using C_Bool.API.DAL.Entities;
using C_Bool.API.DTOs;

namespace C_Bool.API.Services
{
    public interface IUserReportService
    {
        void CreateReportEntry(UserReport userReport);
        void UpdateReportEntry(UserReport userReport);
        UserReport GetReportEntryByUserId(int userId);
        public GetPartialCountDto ActiveUsersCount();
    }
}