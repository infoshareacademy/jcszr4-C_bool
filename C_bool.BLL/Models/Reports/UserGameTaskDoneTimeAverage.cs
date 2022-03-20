using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace C_bool.BLL.Models.Reports
{
    public class UserGameTaskDoneTimeAverage
    {
        [JsonProperty(PropertyName = "average")]
        public double TimeSpan { get; set; }
        public TimeSpan AverageDoneTime { get; set; }

        public UserGameTaskDoneTimeAverage()
        {
            AverageDoneTime = new TimeSpan();

            AverageDoneTime = System.TimeSpan.FromMilliseconds(TimeSpan);

        }
    }
}
