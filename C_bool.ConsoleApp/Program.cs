using System;
using System.Drawing;

namespace C_bool.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {

            //z googla wg podanych parametrów https://developers.google.com/maps/documentation/places/web-service/search-nearby
            var keyword = "nocny";              //
            var latitude = "54.3654101";        // tu Gdańsk
            var longitude = "18.6164213";       
            var radius = 10000;                 //10 kilometrów
            var type = "club";                  //kategoria

            

            // tworzy zapytanie do Googla i zwraca Stream
            var webStream = C_bool.BLL.JsonUtils.GetStreamFromGoogle(keyword, latitude, longitude, radius, type);

            // ...lub Stream z pliku json
            var jsonStream = C_bool.BLL.JsonUtils.ReadStream("places.json");

            //deserializuje do klasy Places na podstawie wybranego Streama (jsonStream lub webStream)
            var places = C_bool.BLL.JsonUtils.GetPlacesFromJson(webStream);
            //var places = C_bool.BLL.JsonUtils.GetPlacesFromJson(jsonStream);


            // proof że bangla :-)
            foreach (var place in places.results)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"NAZWA: {place.name}");
                Console.ResetColor();
                Console.WriteLine($"\t | OCENA: { place.rating} (wszystkich ocen: { place.user_ratings_total}) \n\t | ADRES: { place.vicinity} \n\t | WSPOLRZEDNE: { place.geometry.location.lat}, { place.geometry.location.lng}");
            }
        }
    }
}
