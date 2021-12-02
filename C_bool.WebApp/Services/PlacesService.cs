using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using C_bool.BLL.Models.Places;
using C_bool.BLL.Repositories;

namespace C_bool.WebApp.Services
{
    public class PlacesService
    {
        public IPlacesRepository Places;
        public List<Place> TempPlaces;

        public PlacesService(IPlacesRepository repository)
        {
            Places = repository;
        }

        public List<Place> GetAll()
        {
            return Places.GetAll();
        }
    }
}
