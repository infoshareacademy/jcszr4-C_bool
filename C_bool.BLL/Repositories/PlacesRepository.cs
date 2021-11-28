using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading;
using C_bool.BLL.Logic;
using C_bool.BLL.Models.Places;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace C_bool.BLL.Repositories
{
    public interface IPlacesRepository : IRepository<Place>
    {

    }

    public sealed class PlacesRepository : BaseRepository<Place>, IPlacesRepository
    {
        public override string FileName { get; } = "places.json";

        public List<Place> SearchByName(string searchName)
        {
            return (from place in Repository where place.Name.ToLower().Contains(searchName.ToLower()) select place)
                .ToList();
        }

        private string TrimJson(string convertedJson, string sectionToGet)
        {
            try
            {
                return JObject.Parse(convertedJson)[sectionToGet]?.ToString();
            }
            catch
            {
                return null;
            }
        }

        protected override string ConvertFileJsonToString() => TrimJson(base.ConvertFileJsonToString(), "results");

    }
}