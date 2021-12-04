using C_bool.WebApp.Interfaces;
using System;
using System.Threading.Tasks;
using C_bool.WebApp.Enums;

namespace C_bool.WebApp.Services
{
    public class GameTaskService : IGameTaskService
    {
        private readonly IGameTasksRepository _gameTasksRepository;
        private IGameTaskService _gameTaskServiceImplementation;

        public GameTaskService(IGameTasksRepository gameTasksRepository)
        {
            _gameTasksRepository = gameTasksRepository;
        }

        public string TaskToBeDone()
        {
            throw new NotImplementedException();
        }

        public string CreateANewTask()
        {
            string selectFromTaskType = "FirstComeFirstServed";
            //if (selectFromTaskType.Equals(TaskType.FirstComeFirstServed))
            //{
            //}
            throw new NotImplementedException();
        }
        // takie coś kaząło mi utworzyć...
        public void ManuallyCompleteTask()
        {
            _gameTaskServiceImplementation.ManuallyCompleteTask();
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
            var task = _gameTasksRepository.Get(taskId);
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