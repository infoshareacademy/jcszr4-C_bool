using C_Bool.API.DAL.Entities;

namespace C_Bool.API.Services
{
    public interface IUserReportService
    {
        void CreateReportEntry(UserReport userReport);
        void UpdateReportEntry(UserReport userReport);
        UserReport GetReportEntryByUserId(int userId);
        public int NumberOfActiveUsers();
    }
}