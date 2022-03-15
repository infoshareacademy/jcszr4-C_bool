using System.Threading.Tasks;
using C_bool.BLL.DAL.Entities;

namespace C_bool.BLL.Services
{
    public interface IReportService
    {
        Task CreateUserReportEntry(User user);
        Task UpdateUserReportEntry(User user);
        Task CreatePlaceReportEntry(Place place);
        Task UpdatePlaceReportEntry(Place place);
        Task CreateGameTaskReportEntry(GameTask gameTask);
        Task UpdateGameTaskReportEntry(GameTask gameTask); 
        Task CreateUserGameTaskReportEntry(UserGameTask userGameTask);
        Task UpdateUserGameTaskReportEntry(UserGameTask userGameTask);
    }
}