using System;
using C_bool.BLL.DAL.Entities;
using C_bool.BLL.Enums;

namespace C_bool.BLL.Services
{
    public interface IGameTaskService
    {
        public UserGameTask GetById(int taskId);
        public GameTaskStatus CompleteTask(int taskId, int userId, out string message);
        public UserGameTask GetUserGameTaskByIds(int userId, int gameTaskId);
        public void AddBonusPoints(int userId, int taskId, int bonusPoints);
    }
}