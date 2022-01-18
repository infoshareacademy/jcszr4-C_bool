using System;
using C_bool.BLL.Validators;

namespace C_bool.ConsoleApp.ConsoleHelpers
{
    public static class ReadDataFromConsole
    {
        public static string GetApiKeyFromConsole()
        {
            Console.WriteLine("\nPodaj klucz API");

            return Console.ReadLine();
        }

        public static double GetLatitudeFromConsole()
        {
            Console.WriteLine("Podaj szerokość geogariczną Twojej lokacji:");
            var input = Console.ReadLine();

            if (PlaceValidator.ValidateLatitude(input, out var message))
            {
                return double.Parse(input);
            }
            else
            {
                if (ConfirmPrompt(message))
                {
                    return GetLatitudeFromConsole();
                }
                else
                {
                    var defaultLatitude = 52.232434503602555;
                    Console.WriteLine($"Ustawiono wartość domyślną: {defaultLatitude}");
                    return defaultLatitude;
                }
            }
        }

        public static double GetLongitudeFromConsole()
        {
            Console.WriteLine("\nPodaj długość  geograficzną Twojej lokacji:");
            var input = Console.ReadLine();

            if (PlaceValidator.ValidateLongitude(input, out var message))
            {
                return double.Parse(input);
            }
            else
            {
                if (ConfirmPrompt(message))
                {
                    return GetLongitudeFromConsole();
                }
                else
                {
                    var defaultLongitude = 21.00488390170703;
                    Console.WriteLine($"Ustawiono wartość domyślna: {defaultLongitude}");
                    return defaultLongitude;
                }
            }
        }

        public static double GetRadiusFromConsole()
        {
            Console.WriteLine("\nPodaj promień w jakim chcesz szukać miejsc w okolicy: [m]");
            var input = Console.ReadLine();

            if (PlaceValidator.ValidateRadius(input, out var message))
            {
                return Math.Abs(double.Parse(input));
            }
            else
            {
                if (ConfirmPrompt(message))
                {
                    return GetRadiusFromConsole();
                }
                else
                {
                    var defaultRadius = 1000.0;
                    Console.WriteLine($"Ustawiono wartość domyślna: {defaultRadius}");
                    return defaultRadius;
                }
            }
        }

        public static int GetMinPointsFromConsole()
        {
            Console.Write("Podaj minimalną ilość punktów: ");
            var input = Console.ReadLine();

            if (UserValidator.ValidatePoints(input, out var message))
            {
                return int.Parse(input);
            }
            else
            {
                if (ConfirmPrompt(message))
                {
                    return GetMinPointsFromConsole();
                }
                else
                {
                    var defaultPoints = 0;
                    Console.WriteLine($"Ustawiono wartość domyślna: {defaultPoints}");
                    return defaultPoints;
                }
            }
        }

        public static int GetMaxPointsFromConsole()
        {
            Console.Write("Podaj maksynalną ilość punktów: ");
            var input = Console.ReadLine();

            if (UserValidator.ValidatePoints(input, out var message))
            {
                return int.Parse(input);
            }
            else
            {
                if (ConfirmPrompt(message))
                {
                    return GetMaxPointsFromConsole();
                }
                else
                {
                    var defaultPoints = int.MaxValue;
                    Console.WriteLine($"Ustawiono wartość domyślna: {defaultPoints}");
                    return defaultPoints;
                }
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
                Console.Write($"\r{description} [t/n] ");
                key = Console.ReadKey(false).Key;
            } while (key != ConsoleKey.T && key != ConsoleKey.N);

            Console.WriteLine();
            return (key == ConsoleKey.T);
        }
    }
}