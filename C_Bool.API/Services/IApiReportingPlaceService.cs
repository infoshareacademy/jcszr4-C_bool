using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using C_Bool.API.DAL.Entities;

namespace C_Bool.API.Services
{
    public interface IApiReportingPlaceService
    {
        public IEnumerable<Place> GetPlaces();
        public void Add(Place place);
        public Place GetPlace(int id);
        public IEnumerable<string> TopListPlaces(int seats);
    }
}
