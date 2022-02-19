using System;
using System.Collections.Generic;
using C_bool.BLL.Enums;

namespace C_bool.BLL.DAL.Entities
{
    public class GameTask : Entity
    {
        public Place Place { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string AfterDoneMessage { get; set; }
        public string Photo { get; set; }
        public TaskType Type { get; set; }
        public int Points { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidThru { get; set; }
        public bool IsActive { get; set; }
        public int CreatedById { get; set; }
        public string CreatedByName { get; set; }
        public virtual List<UserGameTask> UserGameTasks { get; set; }
        public string TextCriterion { get; set; }
        public bool IsDoneLimited { get; set; } 
        public int LeftDoneAttempts { get; set; }

        public IList<Message> Messages { get; set; }

        public GameTask()
        {
            Messages = new List<Message>();
        }
    }
}