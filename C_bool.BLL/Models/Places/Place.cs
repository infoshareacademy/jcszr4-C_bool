using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace C_bool.BLL.Models.Places
{
    public class Place : IEntity
    {
        [JsonProperty(PropertyName = "place_id")]
        public string Id { get; set; } = Guid.NewGuid().ToString().Replace("-", "");
        public Geometry Geometry { get; set; }
        [DisplayName("Nazwa miejsca")]
        [Required(ErrorMessage = "Miejsce musi posiadać nazwę")]
        public string Name { get; set; }
        [DisplayName("Opis")]
        [Required(ErrorMessage = "Miejsce musi posiadać opis")]
        public string Description { get; set; }
        public List<Photo> Photos { get; set; } = new();

        public List<string> Types { get; set; } = new();
        public double Rating { get; set; } = 0.0;

        [JsonProperty(PropertyName = "user_ratings_total")]
        public int UserRatingsTotal { get; set; } = 0;

        [JsonProperty(PropertyName = "vicinity")]
        [DisplayName("Adres")]
        [Required(ErrorMessage = "Musisz podać adres miejsca, lub jego przybliżoną lokalizację")]
        public string Address { get; set; } = "no_address";

        [JsonProperty("formatted_address")]
        private string AddressFormatted { set { Address = value; } }

        public bool IsUserCreated { get; set; }
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