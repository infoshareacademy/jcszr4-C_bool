using C_Bool.API.DAL.Entities;

namespace C_Bool.API.Services
{
    public interface IGameTaskReportService
    {
        void CreateReportEntry(GameTaskReport gameTaskReport);
        void UpdateReportEntry(GameTaskReport gameTaskReport);
        GameTaskReport GetReportEntryByGameTaskId(int gameTaskId);
    }
}