using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace C_bool.BLL.Models.Reports
{
    public class UserGameTaskByUsersClassification
    {
        public List<UserGameTaskByUsersCount> UserGameTaskByUsersCountList { get; set; }
    }

    public class UserGameTaskByUsersCount
    {
        [JsonProperty(PropertyName = "name")]
        public string GameTaskName { get; set; }

        [JsonProperty(PropertyName = "count")]
        public int UsersCount { get; set; }
    }
}
