using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace C_bool.WebApp.Interfaces
{
    public interface IGameTaskService
    {
        string TaskToBeDone();
        string CreateANewTask(); //zdefiniować w tasku w klasie
    }
}
