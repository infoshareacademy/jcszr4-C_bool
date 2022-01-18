using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace C_bool.BLL.Models.GooglePlaces
{
    public class GooglePlace
    {
        [JsonProperty(PropertyName = "place_id")]
        public string Id { get; set; }
        public Geometry Geometry { get; set; }
        public string Name { get; set; }

        [JsonProperty(PropertyName = "photos")]
        public List<Photo> GooglePhotos { get; set; } = new();
        public List<string> Types { get; set; } = new();
        public double Rating { get; set; }

        [JsonProperty(PropertyName = "user_ratings_total")]
        public int UserRatingsTotal { get; set; }

        [JsonProperty(PropertyName = "vicinity")]
        public string Address { get; set; } = "no_address";

        [JsonProperty("formatted_address")]
        private string AddressFormatted { set { Address = value; } }
    }

    public class Photo
    {
        public int Height { get; set; }

        [JsonProperty(PropertyName = "html_attributions")]
        public List<string> HtmlAttributions { get; set; }

        [JsonProperty(PropertyName = "photo_reference")]
        public string PhotoReference { get; set; }
        public int Width { get; set; }
    }
}