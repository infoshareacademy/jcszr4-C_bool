using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using C_bool.BLL.Logic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace C_bool.WebApp.Models
{
    public class NameSearchRequest
    {
        [DisplayName("Szukana fraza")] 
        public string SearchPhrase { get; set; }

    }
}
