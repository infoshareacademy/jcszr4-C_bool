using System.ComponentModel.DataAnnotations;

namespace C_bool.BLL.Enums
{
    public enum TaskType
    {
        //FirstComeFirstServed,
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
