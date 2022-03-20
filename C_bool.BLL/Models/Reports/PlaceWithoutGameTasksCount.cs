using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace C_bool.BLL.Models.Reports
{
    public class PlaceWithoutGameTasksCount
    {
        [JsonProperty(PropertyName = "partialCount")]
        public int PlacesCountWithoutGameTask { get; set; }

        [JsonProperty(PropertyName = "totalCount")]
        public int TotalPlacesCount { get; set; }
    }
}
