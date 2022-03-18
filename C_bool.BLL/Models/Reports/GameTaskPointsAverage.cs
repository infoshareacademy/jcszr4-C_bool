using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace C_bool.BLL.Models.Reports
{
    public class GameTaskPointsAverage
    {
        [JsonProperty(PropertyName = "average")]
        public int PointsAverage { get; set; }
    }
}
