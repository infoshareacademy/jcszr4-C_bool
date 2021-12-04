using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading;
using C_bool.BLL.Logic;
using C_bool.BLL.Models;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace C_bool.BLL.Repositories
{
    public sealed class GameTasksRepository : BaseRepository<GameTask>, IGameTasksRepository
    {
        public override string FileName { get; } = "places.json";

        public List<GameTask> SearchByName(string searchName)
        {
            return (from gameTask in Repository where gameTask.Name.ToLower().Contains(searchName.ToLower()) select gameTask)
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