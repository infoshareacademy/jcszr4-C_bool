using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using C_bool.BLL.Models.Places;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace C_bool.WebApp.Models
{
    public class PlaceViewModel
    {
        public string Id { get; set; }
        public Geometry Geometry { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
    }
}
