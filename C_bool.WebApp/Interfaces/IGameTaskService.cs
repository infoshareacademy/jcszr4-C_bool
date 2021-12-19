using C_bool.BLL.DAL.Entities;


namespace C_bool.WebApp.Interfaces
{
    public interface IGameTaskService
    {
        GameTask CreateANewTask(); //zdefiniować w tasku w klasie
        void AddTaskToRepository(GameTask task);
        void ManuallyCompleteTask();
    }
}
