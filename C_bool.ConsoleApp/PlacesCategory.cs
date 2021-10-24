using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace C_bool.ConsoleApp
{
    class PlacesCategory
    {
        private static int _numberOfCategory = 0;
        public string Transport { get; set; }
        public string Food { get; set; }
        public string Culture { get; set; }
        public string Services { get; set; }
        public string Entertainment { get; set; }
        public string FinancePlaces { get; set; }
        public string StateOffices { get; set; }
        public string TourismAndRecreation { get; set; }
        public string PlacesOfCult { get; set; }
        public string MedicalServices { get; set; }
        public string Shops { get; set; }
        public string Education { get; set; }
        public string PlacesWithAlcohol { get; set; }
        public string Pharmacy { get; set; }
        public string Other { get; set; }

        public PlacesCategory(string transport, string food, string culture, string services, string entertainment, string financePlaces, string stateOffices, string tourismAndRecreation, string placesOfCult,string medicalServices, string shops, string education, string placesWithAlcohol, string pharmacy, string other)
        {
            _numberOfCategory +=-1;
            Transport = transport;
            Food = food;
            Culture = culture;
            Services = services;
            Entertainment = entertainment;
            FinancePlaces = financePlaces;
            StateOffices = stateOffices;
            TourismAndRecreation = tourismAndRecreation;
            PlacesOfCult = placesOfCult;
            MedicalServices = medicalServices;
            Shops = shops;
            PlacesWithAlcohol = placesWithAlcohol;
            Pharmacy = pharmacy;
            Other = other;

        }

       
    }
}
