using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace C_bool.BLL
{
    public static class JsonUtils
    {
        private static Stream _jsonStream;
        private static string apiKey = "AIzaSyBLrxme3YgyKcCE8WNryrJrlM_igNRVTeM";
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
                Console.WriteLine("Nie znaleziono pliku: " + ex.Message);
            }
            catch (IOException ex)
            {
                Console.WriteLine("błąd dostępu do pliku: " + ex.Message);
            }

            return _jsonStream;
        }

        public static Stream GetStreamFromGoogle(string keyword, string latitude,string longitude, int radius, string type)
        {
            var webRequest = WebRequest.Create(@$"https://maps.googleapis.com/maps/api/place/nearbysearch/json?keyword={keyword}&location={latitude}%2C{longitude}&radius={radius}&type={type}&key={apiKey}&region={region}&language={language}");

            webRequest.ContentType = "application/json";

            WebResponse response = webRequest.GetResponse();
            Stream data = response.GetResponseStream();

            return data;
        }

        public static Places GetPlacesFromJson(Stream reader)
        {
            var places = new Places();

            try
            {
                using (var streamReader = new StreamReader(reader))
                {
                    var readerData = streamReader.ReadToEnd();
                    places = (Places)JsonConvert.DeserializeObject<Places>(readerData);
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine("błąd dostępu do pliku: " + ex.Message);
            }

            return places;
        }

    }

}
