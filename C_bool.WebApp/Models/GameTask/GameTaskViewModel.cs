using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace C_bool.WebApp.Models.GameTask
{
    public class GameTaskViewModel
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
            [DisplayName("Dodaj zdjęcie")]
            public string Photo { get; set; }
            [DisplayName("Zadanie")]
            [Required(ErrorMessage = "Musisz wybrać zadanie z listy")]
            public List<SelectListItem> Type { get; set; }
            //TODO: musi być minmum dzisiaj
            public DateTime ValidFrom { get; set; }
            //TODO: musi byc wieksze niz valid from
            public DateTime ValidThru { get; set; }
            public string TextCriterion { get; set; }
        }
    }
