using System.Collections.Generic;
using C_bool.BLL.Models.Places;

namespace C_bool.BLL.Repositories
{
    public interface IPlacesRepository : IRepository<Place>
    {
        public List<Place> SearchByName(string searchId);
    }
}