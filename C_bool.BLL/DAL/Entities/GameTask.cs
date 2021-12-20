using System;
using C_bool.BLL.Enums;

namespace C_bool.BLL.DAL.Entities
{
    public class GameTask : Entity
    {
        public Place Place { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
        public TaskType Type { get; set; }
        public int Points { get; set; }
        public DateTime ValidFrom { get; set; } //dogadamy
        public DateTime ValidThru { get; set; }
        public bool IsActive { get; set; }
        public string CreatedById { get; set; }
        public string CreatedByName { get; set; }
    }

    //Metoda jeżeli kryterium jest spełnione {}
    //Metoda jeżeli kryterium nie jest spełnione {}
}