using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using C_bool.BLL.Models;


namespace C_bool.WebApp.Interfaces
{
    public interface IGameTaskService
    {
        GameTask CreateANewTask(); //zdefiniować w tasku w klasie
        void AddTaskToRepository(GameTask task);
        void ManuallyCompleteTask();
    }
}
