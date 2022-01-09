using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using C_bool.BLL.Enums;
using Newtonsoft.Json;

namespace C_bool.BLL.DAL.Entities
{
    public class User : Entity
    {
        [Required(ErrorMessage = "Nazwa jest wymagana")]
        [StringLength(30, ErrorMessage = "Nazwa powinna zawierać od 2 do 30 znaków")]
        [Display(Name = "Nazwa użytkownika")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email jest wymagany")]
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
        [Display(Name = "Płeć")]
        public Gender Gender { get; set; }
        [Display(Name = "Aktywny")]
        public bool IsActive { get; set; }
        [JsonProperty(PropertyName = "registered")]
        [Display(Name = "Data rejestracji")]
        public DateTime CreatedOn { get; set; }
        [Display(Name = "Liczba punktów")]
        public int Points { get; set; }
        public List<Place> FavPlaces { get; set; }
        public List<GameTask> UserGameTasks { get; set; }
        public List<GameTask> UserDoneTasks { get; set; }
    }
}
