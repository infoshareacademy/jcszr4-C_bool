using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C_bool.BLL.Enums;

namespace C_bool.BLL.Logic
{
    class TaskTypeTranslated
    {
        public static Dictionary<TaskType, string> EnumTaskTypeTranslated { get; set; } = new()
        {
            { TaskType.FirstComeFirstServed, "Kto pierwszy ten lepszy" },
            { TaskType.TakeAPhoto, "Zrób zdjęcie" },
            { TaskType.CheckInToALocation, "Zamelduj się w wybranym miejscu" },
            { TaskType.Event, "Zameldowanie o określonej godzinie" },
            { TaskType.TextEntry, "Wprowadź tekst" }
        };
    }
}
