using System.Collections.Generic;
using C_bool.BLL.DAL.Entities;
using C_bool.BLL.Enums;


namespace C_bool.BLL.Services
{
    public interface IUserService
    {
        int GetCurrentUserId();
        User GetCurrentUser();
        void AddFavPlace(Place place);
        void AddTaskToUser(GameTask gameTask);
        public List<Place> GetFavPlaces();
        void SetTaskAsDone(int gameTaskId);
        void SetTaskAsDone(int userId, int gameTaskId);
        void SetUserPoints(User user);
        List<GameTask> GetToDoTasks();
        List<GameTask> GetToDoTasks(int userId);
        List<GameTask> GetInProgressTasks();
        List<GameTask> GetInProgressTasks(int userId);
        List<GameTask> GetDoneTasks();
        List<GameTask> GetDoneTasks(int userId);
        List<User> SearchByName(string name);
        List<User> SearchByEmail(string email);
        List<User> SearchByGender(Gender gender);
        List<User> SearchActive(bool isActive);
        List<User> OrderByPoints(bool isDescending);
        List<User> SearchUsers(string name, string email, string gender, string isActive, string isDescending);

    }
}