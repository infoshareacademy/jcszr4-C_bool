using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using C_bool.BLL.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace C_bool.WebApp.Models.GameTask
{
    public class GameTaskEditModel
    { 
        public int PlaceId { get; set; }
        [DisplayName("Nazwa zadania")]
        [Required(ErrorMessage = "Zadanie musi posiadać nazwę!")]
        public string Name { get; set; }
        [DisplayName("Krótki opis")]
        [MaxLength(500)]
        [Required(ErrorMessage = "Zadanie musi posiadać opis")]
        public string ShortDescription { get; set; }
        [DisplayName("Rozszerzony opis")]
        public string Description { get; set; }
        public string AfterDoneMessage { get; set; }
        [DisplayName("Dodaj zdjęcie")]
        public string Photo { get; set; }
        [DisplayName("Zadanie")]
        [Required(ErrorMessage = "Musisz wybrać zadanie z listy")]
        public TaskType Type { get; set; }
        public int Points { get; set; }
        [DisplayName("Data początkowa")]
        public DateTime ValidFrom { get; set; }
        [DisplayName("Data końcowa")]
        public DateTime ValidThru { get; set; }

        public bool IsActive { get; set; } = true;
        [DisplayName("Tajemne hasło")]
        public string TextCriterion { get; set; }
        public bool IsDoneLimited { get; set; }
        [DisplayName("Ogranicz liczbę użytkowników")]
        public int? LeftDoneAttempts { get; set; }
    }
}
