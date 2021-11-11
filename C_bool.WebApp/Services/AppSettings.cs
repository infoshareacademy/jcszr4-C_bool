using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using C_bool.WebApp.Interfaces;
using Microsoft.Extensions.Configuration;

namespace C_bool.WebApp.Services
{
    public class AppSettings : IAppSettings
    {
        public string GoogleAPIKey { get; set; }

    }
}
