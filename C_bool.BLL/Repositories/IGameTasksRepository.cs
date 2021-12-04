using System.Collections.Generic;
using C_bool.BLL.Models;
using C_bool.BLL.Models.Places;

namespace C_bool.BLL.Repositories
{
    public interface IGameTasksRepository : IRepository<GameTask>
    {
        public List<GameTask> SearchByName(string searchId);
    }
}