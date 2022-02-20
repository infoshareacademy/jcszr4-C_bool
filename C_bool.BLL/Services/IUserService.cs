using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using C_bool.BLL.DAL.Entities;
using C_bool.BLL.Enums;


namespace C_bool.BLL.Services
{
    public interface IUserService
    {
        IQueryable<User> GetAllQueryable();
        int GetCurrentUserId();
        User GetCurrentUser();
        Task<List<string>> GetUserRoles();
        Task<List<string>> GetUserRoles(int id);
        int GetRankingPlace();
        void ChangeUserStatus(int userId);

        bool AddFavPlace(Place place);
        bool RemoveFavPlace(Place place);
        List<Place> GetFavPlaces();
        List<Place> GetPlacesCreatedByUser();

        bool AddTaskToUser(GameTask gameTask);
        List<GameTask> GetAllTasks();
        List<GameTask> GetToDoTasks();
        List<GameTask> GetToDoTasks(int userId);
        List<GameTask> GetInProgressTasks();
        List<GameTask> GetInProgressTasks(int userId);
        List<GameTask> GetDoneTasks();
        List<GameTask> GetDoneTasks(int userId);
        List<GameTask> GetTasksToAccept();
        List<GameTask> GetTasksToAccept(int userId);
        List<GameTask> GetGameTasksCreatedByUser();

        List<User> SearchByName(string name);
        List<User> SearchByEmail(string email);
        List<User> SearchByGender(Gender gender);
        List<User> SearchActive(bool isActive);
        List<User> OrderByPoints(bool isDescending);
        List<User> SearchUsers(string name, string email, string gender, string isActive, string isDescending);
        bool PostMessage(int userId, Message message);
    }
}