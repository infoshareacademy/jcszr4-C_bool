﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using C_bool.BLL.Logic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace C_bool.WebApp.Models
{
    public class PlaceSearchRequest
    {
        [DisplayName("Szerokość geograficzna")]
        public string Latitude { get; set; }
        [DisplayName("Długość geograficzna")]
        public string Longitude { get; set; }
        [DisplayName("Promień")]
        public int Radius { get; set; } = 2000;
        public List<string> Type { get; set; }

        public string SelectedType { get; set; } = "";

        public List<SelectListItem> ListItems { get; set; }
        


        public PlaceSearchRequest()
        {
            Type = new List<string>();

            var list = SearchPlaceByCategory.PlaceCategories.Values.ToList();
            foreach (var item in list)
            {
                Type.AddRange(item);
            }

            ListItems = Type.Select(x => new SelectListItem { Text = x, Value = x })
                .ToList();
        }
    }
}
