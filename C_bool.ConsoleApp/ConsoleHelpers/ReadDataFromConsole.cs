using System;
using System.Collections.Generic;
using C_bool.BLL.Logic;
using C_bool.BLL.Models;
using C_bool.BLL.Repositories;

namespace C_bool.ConsoleApp.ConsoleHelpers
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

        /// <summary>
        /// Asks user for yes/no confirmation and returns true/false
        /// </summary>
        /// <param name="description"></param>
        /// <returns>bool true or false based on user choice</returns>
        public static bool ConfirmPrompt(string description)
        {
            ConsoleKey key;
            do
            {
                Console.Write($"{ description } [t/n] ");
                key = Console.ReadKey(false).Key;
            } while (key != ConsoleKey.T && key != ConsoleKey.N);
            return (key == ConsoleKey.T);
        }

        public static void GetUserClassification(UsersRepository repository)
        {
            int minPoints;
            int maxPoints;

            Console.Write("Podaj minimalną ilość punktów użytkownika: ");
            var input = Console.ReadLine();
            while (!int.TryParse(input, out minPoints))
            {
                Console.Write($"\nWprowadzona wartość ({input}) jest nieprawidłowa (tylko liczby), spróbuj ponownie: ");
                input = Console.ReadLine();
            }

            Console.Write("Podaj maksymalną ilość punktów użytkownika: ");
            input = Console.ReadLine();
            while (!int.TryParse(input, out maxPoints) && maxPoints < minPoints)
            {
                Console.Write($"\nNieprawidłowa wartość! Spróbuj jeszcze raz.");
                input = Console.ReadLine();
            }
            var sortDescending = ConfirmPrompt("Czy chcesz sortować w kolejności malejącej?");
            var usersByPoints = SearchUsers.ByPointsRange(repository.Repository, minPoints, maxPoints, sortDescending);
            Console.WriteLine($"\n\nUŻYTKOWNICY Z MIN {minPoints} i MAX {maxPoints} ILOŚCIĄ PUNKTÓW, SORTOWANE {(sortDescending ? "MALEJĄCO" : "ROSNĄCO")}:");
            GetInfo.UserInformation(usersByPoints, "");
        }
    }
}
