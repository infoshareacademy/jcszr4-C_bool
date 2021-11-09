using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using C_bool.BLL.Models.Places;
using C_bool.BLL.Repositories;

namespace C_bool.WebApp.Services
{
    public class MapService
    {
        public static List<Place> Places = new List<Place>();

        public void GetFromRepo(BaseRepository<Place> repository)
        {
            Places = repository.Repository;
        }

        public List<Place> GetAll()
        {
            return Places;
        }
    }
}
