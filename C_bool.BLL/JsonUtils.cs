using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace C_bool.BLL
{
    public static class JsonUtils
    {
        private static Stream _jsonStream;
        private static string region = "PL";
        private static string language = "pl";

        public static Stream ReadStream(string filePath)
        {
            try
            {
                _jsonStream = new StreamReader(filePath).BaseStream;
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"File {filePath} not found: {ex.Message}");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Cannot access file {filePath}: {ex.Message}");
            }

            return _jsonStream;
        }

        public static Stream GetStreamFromGoogle(string keyword, string latitude, string longitude, int radius, string type)
        {
            Console.Write("Wpisz swój apiKey: ");
            var apiKey = Console.ReadLine();
            var webRequest = WebRequest.Create(@$"https://maps.googleapis.com/maps/api/place/nearbysearch/json?keyword={keyword}&location={latitude}%2C{longitude}&radius={radius}&type={type}&key={apiKey}&region={region}&language={language}");

            webRequest.ContentType = "application/json";
            try
            {
                Console.WriteLine($"Wait from server response: {webRequest.RequestUri}");
                WebResponse response = webRequest.GetResponse();
                Stream data = response.GetResponseStream();
                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Server response error: {ex.Message}");
            }

            return Stream.Null;

        }

        public static Places GetPlacesFromJson(Stream reader)
        {
            var places = new Places();

            try
            {
                using var streamReader = new StreamReader(reader);
                var readerData = streamReader.ReadToEnd();
                places = (Places)JsonConvert.DeserializeObject<Places>(readerData);
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Stream access denied: { ex.Message}");
            }

            return places;
        }

        public static List<Users> GetUsersFromJson(Stream reader)
        {
            var users = new List<Users>();

            try
            {
                using var streamReader = new StreamReader(reader);
                var readerData = streamReader.ReadToEnd();
                users = JsonConvert.DeserializeObject<List<Users>>(readerData);
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Stream access denied: { ex.Message}");
            }

            return users;
        }

        public static void PrintUsersInformation(List<Users> list, string Id)
        {
            foreach (var user in list)
            {
                var outputString = $"\t| Imię: {user.firstName}\n\t| Nazwisko: {user.lastName}\n\t| Płeć: {user.gender}\n\t| Wiek: {user.age}\n\t| Adres: {user.address}\n\t| E-mail: {user.email}\n\t| Telefon: {user.phone}\n\t| Firma: {user.company}\n\t------------\n\t| Aktywny: {user.isActive}\n\t| Szer. geo.: {user.latitude}\n\t| Wys. geo.: {user.longitude}\n\t| Zarejestrowany: {user.registered}\n";

                if (user.Id.Equals(Id))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"ID: {user.Id}");
                    Console.ResetColor();
                    Console.Write(outputString);
                    return;
                }
                else if (Id.Length == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"ID: {user.Id}");
                    Console.ResetColor();
                    Console.Write(outputString);
                }
            }
        }

        public static void PrintPlacesInformation(Places places, string Id)
        {
            foreach (var place in places.results)
            {
                var outputString = $"\t| Ocena: {place.rating} (wszystkich ocen: {place.user_ratings_total})\n\t| Adres: {place.vicinity}\n\t| Szer. geo.: {place.geometry.location.lat}\n\t| Wys. geo.: {place.geometry.location.lng}\n";

                if (place.place_id.Equals(Id))
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"NAZWA: {place.name}");
                    Console.ResetColor();
                    Console.Write(outputString);
                    return;
                }
                else if (Id.Length == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"NAZWA: {place.name}");
                    Console.ResetColor();
                    Console.Write(outputString);
                }
            }
        }

    }

}
