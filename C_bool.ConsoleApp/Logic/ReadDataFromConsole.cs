using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C_bool.BLL.Repositories;

namespace C_bool.ConsoleApp.Logic
{
    public static class ReadDataFromConsole
    {
        public static double Latitude { get; private set; }
        public static double Longitude { get; private set; }
        public static double Radius { get; private set; }
        public static string ApiKey { get; private set; } = "AIzaSyAu7_b5Cy5_D1ulYr609G7Rjvzn-FD6dJM";


        private static void SetApiKeyFromConsole()
        {
            Console.WriteLine("\nPodaj klucz API");
            ApiKey = Console.ReadLine();
        }

        private static void SetLongitudeFromConsole()
        {
            Console.WriteLine("\nPodaj szerokość geograficzną Twojej lokacji:");
            if (double.TryParse(Console.ReadLine().Replace(',', '.'), out double longitude))
            {
                Longitude = longitude;
            }
            else
            {
                Console.WriteLine("Nieprawidlowa wartosc, spróbuj jeszcze raz");
                SetNearbyPlacesApiParameters();
            }
        }

        private static void SetLatitudeFromConsole()
        {
            Console.WriteLine("Podaj szerokość geogariczną Twojej lokacji:");
            if (double.TryParse(Console.ReadLine().Replace(',', '.'), out double latitude))
            {
                Latitude = latitude;
            }
            else
            {
                Console.WriteLine("Nieprawidlowa wartosc, spróbuj jeszcze raz");
                SetNearbyPlacesApiParameters();
            }
        }

        public static void SetRadiusFromConsole()
        {
            Console.WriteLine("\nPodaj promień w jakim chcesz szukać miejsc w okolicy: [m]");
            
            if (double.TryParse(Console.ReadLine().Replace(',' , '.'), out double radius))
            {
                Radius = radius;
            }
            else
            {
                Console.WriteLine("Nieprawidlowa wartosc, spróbuj jeszcze raz");
                SetRadiusFromConsole();
            }
        }

        public static void SetNearbyPlacesApiParameters()
        {
            SetLatitudeFromConsole();
            SetLongitudeFromConsole();
            SetRadiusFromConsole();
            SetApiKeyFromConsole();
        }
    }
}
