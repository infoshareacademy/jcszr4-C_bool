using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq.Expressions;
using System.Net;
using C_bool.BLL.Models.Places;
using Newtonsoft.Json;

namespace C_bool.BLL.Repositories
{
    public class PlacesRepository : IRepository<Place>
    {
        [JsonProperty(PropertyName = "results")]
        public List<Place> Places { get; private set; }

        public PlacesRepository()
        {
            Places = new List<Place>();
        }

        private static string region = "PL";
        private static string language = "pl";

        public void AddFileDataToRepository()
        {
            try
            {
                var jsonStream = new StreamReader("places.json").BaseStream;
                var streamReader = new StreamReader(jsonStream);
                var repository = JsonConvert.DeserializeObject<PlacesRepository>(streamReader.ReadToEnd());

                if (repository == null)
                {
                    Console.WriteLine("Plik jest pusty!");
                }
                else
                {
                    foreach (var place in repository.Places)
                    {
                        Places.Add(place);
                    }
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("Nie znaleziono pliku: " + ex.Message);
            }
            catch (IOException ex)
            {
                Console.WriteLine("Błąd dostępu do pliku: " + ex.Message);
            }
        }

        public void AddFileDataToRepository(string keyword, double latitude, double longitude, int radius, string type)
        {
            var latitudeString = latitude.ToString(CultureInfo.InvariantCulture);
            var longitudeString = longitude.ToString(CultureInfo.InvariantCulture);

            Console.Write("Wpisz swój apiKey: ");
            var apiKey = Console.ReadLine();
            var webRequest = WebRequest.Create(@$"https://maps.googleapis.com/maps/api/place/nearbysearch/json?keyword={keyword}&location={latitudeString}%2C{longitudeString}&radius={radius}&type={type}&key={apiKey}&region={region}&language={language}");

            webRequest.ContentType = "application/json";
            try
            {
                Console.WriteLine($"Wait from server response: {webRequest.RequestUri}");
                WebResponse response = webRequest.GetResponse();
                Stream data = response.GetResponseStream();
                if (data != null)
                {
                    var streamReader = new StreamReader(data);
                    var repository = JsonConvert.DeserializeObject<PlacesRepository>(streamReader.ReadToEnd());

                    if (repository == null)
                    {
                        Console.WriteLine("Plik jest pusty!");
                    }
                    else
                    {
                        foreach (var place in repository.Places)
                        {
                            Places.Add(place);
                        }
                    }
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("Nie znaleziono pliku: " + ex.Message);
            }
            catch (IOException ex)
            {
                Console.WriteLine("Błąd dostępu do pliku: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Server response error: {ex.Message}");
            }
        }

        public IEnumerable<Place> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Place> Find(Expression<Func<Place, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public void Add(Place entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(Place entity)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Place> IRepository<Place>.GetAll()
        {
            throw new NotImplementedException();
        }
    }
}