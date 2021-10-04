using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_bool.BLL
{
    public class Location
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public class Geometry
    {
        public Location location { get; set; }

    }

    public class OpeningHours
    {
        public bool open_now { get; set; }
    }

    public class Result
    {
        public string business_status { get; set; }
        public Geometry geometry { get; set; }
        public string name { get; set; }
        public bool permanently_closed { get; set; }
        public string place_id { get; set; }
        public double rating { get; set; }
        public List<string> types { get; set; }
        public int user_ratings_total { get; set; }
        public string vicinity { get; set; }
        public OpeningHours opening_hours { get; set; }
    }

    public class Places
    {
        public List<object> html_attributions { get; set; }
        public List<Result> results { get; set; }
        public string status { get; set; }
    }
}
