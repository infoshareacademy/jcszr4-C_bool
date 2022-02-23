using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace C_bool.WebApp.Models.GameTask
{
    public class UserGameTaskViewModel
    {
        public int UserId { get; set; }
        public int GameTaskId { get; set; }
        public virtual GameTaskViewModel GameTask { get; set; }
        public bool IsDone { get; set; }
        public DateTime DoneOn { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Photo { get; set; }
        public string TextCriterion { get; set; }
        public DateTime ArrivalTime { get; set; }

    }
}
