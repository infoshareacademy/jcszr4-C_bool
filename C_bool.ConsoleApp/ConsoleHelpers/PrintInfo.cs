using System;
using System.Collections.Generic;
using System.Linq;
using C_bool.BLL.Logic;
using C_bool.BLL.Repositories;
using C_bool.BLL.Validators;
using C_bool.ConsoleApp.Other;

namespace C_bool.ConsoleApp.ConsoleHelpers
{
    class PrintInfo
    {
        public static void UserClassification(UsersRepository usersRepository)
        {
            usersRepository.AssignRandomPointsToUsers(usersRepository); // do testów

            if (usersRepository.IsRepositoryEmpty(usersRepository.Repository))
            {
                Console.WriteLine("Brak wyników do wyświetlenia!");
                return;
            }

            var isDescending = ReadDataFromConsole.ConfirmPrompt("Czy chcesz sortować w kolejności malejącej?");

            if (ReadDataFromConsole.ConfirmPrompt("Czy chcesz określić przedział punktów?"))
            {
                while (true)
                {
                    var minPoints = ReadDataFromConsole.GetMinPointsFromConsole();
                    var maxPoints = ReadDataFromConsole.GetMaxPointsFromConsole();
                    if (UserValidator.ValidatePointsRange(minPoints, maxPoints, out var message))
                    {
                        var usersByPointsRange = SearchUsers.ByPointsRange(
                            usersRepository.Repository,
                            minPoints,
                            maxPoints,
                            isDescending
                        );

                        if (usersRepository.IsRepositoryEmpty(usersByPointsRange))
                        {
                            Console.WriteLine("Brak wyników do wyświetlenia!");
                            return;
                        }

                        Console.WriteLine();
                        GetInfo.UserInformation(usersByPointsRange, "");
                        return;
                    }

                    Console.WriteLine(message);
                }
            }

            var usersOrderByPoints = usersRepository.OrderByPoints(isDescending);

            Console.WriteLine();
            GetInfo.UserInformation(usersOrderByPoints, "");
        }

        public static void PlacesByCategory(PlacesRepository repository)
        {
            var categories = SearchPlaceByCategory.PlaceCategories;
            Console.WriteLine("Dostępne kategorie: ");

            Console.ForegroundColor = ConsoleColor.Cyan;
            foreach (var category in categories)
            {
                var placesWithMatchedCategoriesCount = SearchPlaceByCategory.GetPlaces(repository.Repository, category.Key).Count;
                Console.WriteLine($"  {category.Key} ({placesWithMatchedCategoriesCount})");
            }
            Console.ResetColor();

            Console.WriteLine("\nPodaj nazwę kategorii:");
            var inputCategory = Console.ReadLine();
            while (inputCategory != null && !categories.ContainsKey(inputCategory))
            {
                Console.Write($"\nWprowadzona kategoria ({inputCategory}) jest nieprawidłowa, spróbuj ponownie: ");
                inputCategory = Console.ReadLine();
            }

            var placesWithMatchedCategories = SearchPlaceByCategory.GetPlaces(repository.Repository, inputCategory);

            if (placesWithMatchedCategories.Count == 0)
            {
                Console.WriteLine("Nie znaleziono miejsc w wybranej kategorii");
                return;
            }

            Console.WriteLine($"\nMiejsca w kategorii {inputCategory}:");
            GetInfo.PlaceInformation(placesWithMatchedCategories, "");

            var searchBySubCategory = false;
            if (placesWithMatchedCategories.Count > 1) searchBySubCategory = ReadDataFromConsole.ConfirmPrompt("Czy chcesz zawęzić swój wybór o podkategorie?");

            if (searchBySubCategory)
            {
                Console.WriteLine();
                Console.WriteLine($"Podkategorie w kategorii {inputCategory}: ");

                var arrayTypes = placesWithMatchedCategories.SelectMany(i => i.Types).ToArray();

                HashSet<string> combinedSubcategories = new HashSet<string>(arrayTypes);

                Console.ForegroundColor = ConsoleColor.Cyan;
                combinedSubcategories.ToList().ForEach(item => Console.WriteLine("  " + item));
                Console.ResetColor();

                Console.WriteLine("\nPodaj nazwę podkategorii:");

                var inputSubCategory = Console.ReadLine();
                while (!combinedSubcategories.Contains(inputSubCategory))
                {
                    Console.Write($"\nWprowadzona kategoria ({inputSubCategory}) jest nieprawidłowa, spróbuj ponownie: ");
                    inputSubCategory = Console.ReadLine();
                }

                var placesWithMatchedSubCategories = SearchPlaceByCategory.GetPlacesExactCategory(placesWithMatchedCategories, inputSubCategory);

                if (placesWithMatchedSubCategories.Count == 0)
                {
                    Console.WriteLine("Nie znaleziono miejsc w wybranej podkategorii");
                    return;
                }
                Console.WriteLine($"\nMiejsca w kategorii {inputCategory} o podkategorii {inputSubCategory}:");
                GetInfo.PlaceInformation(placesWithMatchedSubCategories, "");
            }
        }

        public static void PlacesNearby(PlacesRepository placesRepository)
        {
            if (placesRepository.IsRepositoryEmpty(placesRepository.Repository))
            {
                Console.WriteLine("Brak wyników do wyświetlenia!");
                return;
            }

            GetInfo.PlaceInformation(placesRepository.Repository, "");
        }

        public static void PlacesByRadius(PlacesRepository placesRepository)
        {
            if (placesRepository.IsRepositoryEmpty(placesRepository.Repository))
            {
                Console.WriteLine("Brak wyników do wyświetlenia!");
                return;
            }

            var placesNearbyRadius =
                placesRepository.GetNearbyPlacesFromRadius(ReadDataFromConsole.GetRadiusFromConsole());

            if (placesRepository.IsRepositoryEmpty(placesNearbyRadius))
            {
                Console.WriteLine("Brak wyników do wyświetlenia!");
                return;
            }

            GetInfo.PlaceInformation(placesNearbyRadius, "");
        }

    }
}
