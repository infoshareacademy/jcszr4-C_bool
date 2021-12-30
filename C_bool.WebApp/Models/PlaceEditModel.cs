using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace C_bool.WebApp.Models
{
    public class PlaceEditModel
    {
        public int Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        [DisplayName("Nazwa miejsca")]
        [Required(ErrorMessage = "Miejsce musi posiadać nazwę")]
        public string Name { get; set; }
        [DisplayName("Krótki opis")]
        [Required(ErrorMessage = "Miejsce musi posiadać opis")]
        public string ShortDescription { get; set; }
        [DisplayName("Rozszerzony opis")]
        public string Description { get; set; }
        public string[] Types { get; set; }
        [DisplayName("Adres")]
        [Required(ErrorMessage = "Musisz podać adres miejsca, lub jego przybliżoną lokalizację")]
        public string Address { get; set; }
    }
}
