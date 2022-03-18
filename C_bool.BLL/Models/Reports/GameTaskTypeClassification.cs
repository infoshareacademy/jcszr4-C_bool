using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace C_bool.BLL.Models.Reports
{
    public class GameTaskTypeClassification
    {
        [JsonProperty(PropertyName = "name")]
        public string TypeName { get; set; }

        [JsonProperty(PropertyName = "count")]
        public string GameTaskCount { get; set; }
    }
}
