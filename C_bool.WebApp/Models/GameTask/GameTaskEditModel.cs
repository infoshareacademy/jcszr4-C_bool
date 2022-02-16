using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using C_bool.BLL.Enums;
using Microsoft.AspNetCore.Mvc;
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
        public TaskType? Type { get; set; }
        public int Points { get; set; }

        [DisplayName("Data początkowa")]
        [DataType(DataType.DateTime, ErrorMessage = "Niepoprawny format daty")]
        [Remote("IsValid_ValidFromDate", "ValueValidator", HttpMethod = "POST", ErrorMessage = "Wprowadź prawidłową datę", AdditionalFields = nameof(ValidFrom) + "," + nameof(ValidThru))]
        public DateTime? ValidFrom { get; set; }

        [DisplayName("Data końcowa")]
        [DataType(DataType.DateTime, ErrorMessage = "Niepoprawny format daty")]
        [Remote("IsValid_ValidThruDate", "ValueValidator", HttpMethod = "POST", ErrorMessage = "Wprowadź prawidłową datę", AdditionalFields = nameof(ValidFrom) + "," + nameof(ValidThru))]
        public DateTime? ValidThru { get; set; }

        [DisplayName("Tajemne hasło")]
        [Remote("IsValid_TextCriterion", "ValueValidator", HttpMethod = "POST", ErrorMessage = "Wprowadź rozwiązanie zadania", AdditionalFields = nameof(Type))]
        public string TextCriterion { get; set; }

        [DisplayName("Ogranicz liczbę użytkowników")]
        [Range(1, 1000000, ErrorMessage = "Możesz ograniczyć liczbę użytkowników w zakresie od {1} do {2}.")]
        public int? LeftDoneAttempts { get; set; }
        public bool? IsDoneLimited { get; set; }
        public bool IsActive { get; set; }
    }

}
