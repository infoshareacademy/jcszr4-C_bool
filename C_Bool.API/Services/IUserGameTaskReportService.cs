using C_Bool.API.DAL.Entities;

namespace C_Bool.API.Services
{
    public interface IUserGameTaskReportService
    {
        void CreateReportEntry(UserGameTaskReport userGameTaskReport);
        void UpdateReportEntry(UserGameTaskReport userGameTaskReport);
        UserGameTaskReport GetReportEntryByUserGameTaskId(int userGameTaskId);
    }
}