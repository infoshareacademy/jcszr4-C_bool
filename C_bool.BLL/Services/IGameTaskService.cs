using System;
using C_bool.BLL.DAL.Entities;

namespace C_bool.BLL.Services
{
    public interface IGameTaskService
    {
        public GameTask CreateNewTask();
        public void AddTaskToRepository(GameTask task);
        public void ManuallyCompleteTask();
        public void ManuallyCompleteTask(int taskId, int userId);
        public bool CompleteTask(int taskId, int userId, string textToConfirm);
        public void CompleteTask(int taskId, int userId, string latitude, string longitude);
        public bool CompleteTask(int taskId, int userId, string latitude, string longitude, DateTime timeOfVisit);
        public bool CompleteTask(int taskId, int userId);
    }
}