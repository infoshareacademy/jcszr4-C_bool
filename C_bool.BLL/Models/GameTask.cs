using System;
using System.Collections.Generic;
using C_bool.BLL.Enums;

namespace C_bool.BLL.Models
{
    public class GameTask : IEntity
    {
        public string Id { get; set; }
        //Nie mogę przywołać Place.Id
        public Place Place { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
        public TaskType Type { get; set; }
        public int Points { get; set; }
        public DateTime DateCreated = DateTime.Now;
        public DateTime ValidFrom { get; set; } //dogadamy
        public DateTime ValidThru { get; set; }
        public bool IsActive { get; set; }
        public string CreatedById { get; set; }
        public string CreatedByName { get; set; }
    }

    public class Place
    {
        public List<GameTask> Tasks { get; set; }
    }

    //Metoda jeżeli kryterium jest spełnione {}
    //Metoda jeżeli kryterium nie jest spełnione {}
}