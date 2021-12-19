using System;
using System.ComponentModel.DataAnnotations;
using C_bool.BLL.Enums;
using Newtonsoft.Json;

namespace C_bool.BLL.DAL.Entities
{
    public class User : Entity
    {
        [Required(ErrorMessage = "Nazwa jest wymagana")]
        [Range(2, 30, ErrorMessage = "Nazwa powinna zawierać od 2 do 30 znaków")]
        [Display(Name = "Nazwa użytkownika")]
        public string Name { get; set; }
        [Display(Name = "Płeć")]
        public Gender Gender { get; set; }
        [Required(ErrorMessage = "Email jest wymagany")]
        [EmailAddress]
        public string Email { get; set; }
        [Display(Name = "Aktywny")]
        public bool IsActive { get; set; }
        [JsonProperty(PropertyName = "registered")]
        [Display(Name = "Data rejestracji")]
        public DateTime CreatedOn { get; set; }
        [Display(Name = "Liczba punktów")]
        public int Points { get; set; }
    }
}
