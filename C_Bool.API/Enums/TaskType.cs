using System.ComponentModel.DataAnnotations;

namespace C_Bool.API.Enums
{
    public enum TaskType
    {
        [Display(Name = "Zrób zdjęcie")]
        TakeAPhoto,
        [Display(Name = "Odwiedź miejsce")]
        CheckInToALocation,
        [Display(Name = "Wydarzenie")]
        Event,
        [Display(Name = "Podaj hasło")]
        TextEntry
    }
}
