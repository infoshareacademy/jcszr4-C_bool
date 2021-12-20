using C_bool.WebApp.Interfaces;
using System;
using C_bool.BLL.DAL.Entities;
using C_bool.BLL.Repositories;

namespace C_bool.WebApp.Services
{
    public class GameTaskService : IGameTaskService
    {
        private IRepository<GameTask> _gameTasksRepository;

        public GameTaskService(IRepository<GameTask> gameTasksRepository)
        {
            _gameTasksRepository = gameTasksRepository;
        }

        public GameTask CreateANewTask()
        {
            string selectFromTaskType = "FirstComeFirstServed";
            //if (selectFromTaskType.Equals(TaskType.FirstComeFirstServed))
            //{
            //}
            throw new NotImplementedException();
        }

        public void AddTaskToRepository(GameTask task)
        {
            throw new NotImplementedException();
        }

        // takie coś kaząło mi utworzyć...

        public void ManuallyCompleteTask()
        {

        }


        public void ManuallyCompleteTask(int taskId, int userId)
        {
            throw new NotImplementedException();
        }


        public bool CompleteTask(int taskId, int userId, string textToConfirm)
        {
            throw new NotImplementedException();
        }

        ///
        public void CompleteTask(int taskId, int userId, string latitude, string longitude)
        {
            var task = _gameTasksRepository.GetById(taskId);
            throw new NotImplementedException();

        }

        public bool CompleteTask(int taskId, int userId, string latitude, string longitude, DateTime timeOfVisit)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Complete task method overload for task type ....
        /// </summary>
        public bool CompleteTask(int taskId, int userId)
        {
            throw new NotImplementedException();
        }
    }
}