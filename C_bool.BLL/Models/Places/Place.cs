using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace C_bool.BLL.Models.Places
{
    public class Place
    {
        [JsonProperty(PropertyName = "place_id")]
        public string PlaceId { get; set; }

        [JsonProperty(PropertyName = "geometry")]
        public Geometry Geometry { get; set; }
        
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        
        [JsonProperty(PropertyName = "types")]
        public string[] Types { get; set; }
        
        [JsonProperty(PropertyName = "rating")]
        public double Rating { get; set; }
        
        [JsonProperty(PropertyName = "user_ratings_total")]
        public int UserRatingsTotal { get; set; }

        [JsonProperty(PropertyName = "vicinity")]
        public string Address { get; set; }

        /// <summary>
        /// prints basic information about place based on PlaceId, PlaceId might be empty
        /// </summary>
        /// <param name="places">input List of Place objects</param>
        /// <param name="id">Id number of place, if empty - prints all places</param>
        public static void PrintInformation(List<Place> places, string id)
        {
            foreach (var place in places)
            {
                var outputString = $"\t| Ocena: {place.Rating} (wszystkich ocen: {place.UserRatingsTotal})\n\t| Adres: {place.Address}\n\t| Szer. geo.: {place.Geometry.Location.Latitude}\n\t| Wys. geo.: {place.Geometry.Location.Longitude}\n";

                if (place.PlaceId.Equals(id))
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"NAZWA: {place.Name}");
                    Console.ResetColor();
                    Console.Write(outputString);
                    return;
                }
                else if (id.Length == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"NAZWA: {place.Name}");
                    Console.ResetColor();
                    Console.Write(outputString);
                }
            }
        }
    }
}