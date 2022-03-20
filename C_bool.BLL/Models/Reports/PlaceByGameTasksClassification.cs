﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace C_bool.BLL.Models.Reports
{
    public class PlaceByGameTasksClassification
    {
        [JsonProperty(PropertyName = "name")]
        public string PlaceName { get; set; }

        [JsonProperty(PropertyName = "count")]
        public int GameTaskCount { get; set; }
    }
}
