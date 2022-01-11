using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_bool.BLL.Logic
{
    class TaskTypeTranslated
    {
        public static Dictionary<string, string> EnumTaskTypeTranslated { get; set; } = new()
        {
            { "FirstComeFirstServed", "Kto pierwszy ten lepszy" },
            { "TakeAPhoto", "Zrób zdjęcie" },
            { "CheckInToALocation", "Zamelduj się w wybranym miejscu" },
            { "CheckInAtTheSpecifiedTime", "Zameldowanie o określonej godzinie" },
            { "TextEntry", "Wprowadź tekst" }
        };
    }
}
