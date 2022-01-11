using System.Collections.Generic;
using C_bool.BLL.DAL.Entities;
using C_bool.BLL.Enums;


namespace C_bool.BLL.Services
{
    public interface IUserService
    {
        List<User> SearchByName(string name);
        List<User> SearchByEmail(string email);
        List<User> SearchByGender(Gender gender);
        List<User> SearchActive(bool isActive);
        List<User> OrderByPoints(bool isDescending);
        void AddFileDataToRepository();
        void AddUser(User user);
        void AddPoints(User user);
        List<User> SearchUsers(string name, string email, string gender, string isActive, string isDescending);
        void AddFavPlace(User user, Place place);
        void AddTaskToUser(User user, GameTask gameTask);
        void SetTaskAsDone(User user, GameTask gameTask);
        List<GameTask> GetToDoTasks(User user);
        List<GameTask> GetInProgressTasks(User user);
        List<GameTask> GetDoneTasks(User user);

    }
}