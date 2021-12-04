using C_bool.WebApp.Interfaces;
using System;
using System.Threading.Tasks;
using C_bool.BLL.Models;
using C_bool.BLL.Repositories;

namespace C_bool.WebApp.Services
{
    public class GameTaskService : IGameTaskService
    {
        private readonly IGameTasksRepository _gameTasksRepository;

        public GameTaskService(IGameTasksRepository gameTasksRepository)
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


        public void ManuallyCompleteTask(string taskId, string userId)
        {
            throw new NotImplementedException();
        }


        public bool CompleteTask(string taskId, string userId, string textToConfirm)
        {
            throw new NotImplementedException();
        }

        ///
        public void CompleteTask(string taskId, string userId, string latitude, string longitude)
        {
            var task = _gameTasksRepository.SearchById(taskId);
            throw new NotImplementedException();

        }

        public bool CompleteTask(string taskId, string userId, string latitude, string longitude, DateTime timeOfVisit)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Complete task method overload for task type ....
        /// </summary>
        public bool CompleteTask(string taskId, string userId)
        {
            throw new NotImplementedException();
        }
    }
}