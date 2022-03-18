using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace C_bool.BLL.Models.Reports
{
    public class UserGameTaskDoneCount
    {
        [JsonProperty(PropertyName = "partialCount")]
        public int DoneCount { get; set; }

        [JsonProperty(PropertyName = "totalCount")]
        public int TotalCount { get; set; }
    }
}
