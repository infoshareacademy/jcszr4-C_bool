using System.ComponentModel;

namespace C_bool.WebApp.Models.GameTask
{
    public class GameTaskParticipateModel
    {
        [DisplayName("Dodaj zdjęcie")]
        public string Photo { get; set; }

        [DisplayName("Tajemne hasło")]
        public string UserTextCriterion { get; set; }
    }
}
