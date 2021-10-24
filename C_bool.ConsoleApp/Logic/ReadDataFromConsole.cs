using System;

namespace C_bool.ConsoleApp.Logic
{
    public static class ReadDataFromConsole
    {
        public static string GetApiKeyFromConsole()
        {
            Console.WriteLine("\nPodaj klucz API");
            
            return Console.ReadLine();
        }

        public static double GetLongitudeFromConsole()
        {
            Console.WriteLine("\nPodaj długość  geograficzną Twojej lokacji:");
            if (double.TryParse(Console.ReadLine().Replace(',', '.'), out double longitude) && longitude >= -180 && longitude <= 180) 
            {
                return longitude;
            }
            else
            {
                Console.WriteLine("Nieprawidłowa wartość! Spróbuj jeszcze raz.");
                
                return GetLongitudeFromConsole();
            }
        }

        public static double GetLatitudeFromConsole()
        {
            Console.WriteLine("Podaj szerokość geogariczną Twojej lokacji:");
            if (double.TryParse(Console.ReadLine().Replace(',', '.'), out double latitude) && latitude >= -90 && latitude <= 90)
            {
                return latitude;
            }
            else
            {
                Console.WriteLine("Nieprawidłowa wartość! Spróbuj jeszcze raz.");

                return GetLatitudeFromConsole();
            }
        }

        public static double GetRadiusFromConsole()
        {
            Console.WriteLine("\nPodaj promień w jakim chcesz szukać miejsc w okolicy: [m]");
            
            if (double.TryParse(Console.ReadLine().Replace(',' , '.'), out double radius))
            {
                return Math.Abs(radius);
            }
            else
            {
                Console.WriteLine("Nieprawidłowa wartość! Spróbuj jeszcze raz.");
                
                return GetRadiusFromConsole();
            }
        }
    }
}
