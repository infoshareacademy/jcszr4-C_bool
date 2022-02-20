using System;
using System.Linq;
using C_bool.BLL.DAL.Entities;
using C_bool.BLL.Enums;

namespace C_bool.BLL.Services
{
    public interface IGameTaskService
    {

        IQueryable<GameTask> GetAllQueryable();
        IQueryable<UserGameTask> GetAllUserGameTasksQueryable();
        GameTask GetById(int taskId);
        UserGameTask GetUserGameTaskById(int taskId);
        GameTaskStatus ManuallyCompleteTask(int taskId, int userId, int extraPoints, out string message);
        GameTaskStatus CompleteTask(int taskId, int userId, out string message);
        UserGameTask GetUserGameTaskByIds(int userId, int gameTaskId);
        void AddBonusPoints(int userId, int taskId, int bonusPoints);
        void AddBonusPoints(UserGameTask userGameTask, int bonusPoints);
        void Add(GameTask gameTask);
        void Update(GameTask gameTask);
        void UpdateUserGameTask(UserGameTask userGameTask);
        void AssignPropertiesFromParticipateModel(UserGameTask userGameTask, string textCriterion, string base64Image);
    }
}