using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using C_bool.BLL.Models.Places;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace C_bool.BLL.Logic
{
    public class GoogleAPI
    {
        private static string TrimJson(string convertedJson, string sectionToGet)
        {
            try
            {
                return JObject.Parse(convertedJson)[sectionToGet]?.ToString();
            }
            catch
            {
                return null;
            }
        }

        protected static string ConvertApiJsonToString(WebRequest webRequest)
        {
            webRequest.ContentType = "application/json";

            var response = webRequest.GetResponse().GetResponseStream();
            if (response != null)
            {
                var streamReader = new StreamReader(response);
                var convertedToString = streamReader.ReadToEnd();

                return convertedToString;
            }

            return null;
        }

        protected string ConvertFileJsonToString() => TrimJson(ConvertFileJsonToString(), "results");

        public static List<Place> ApiGetNearbyPlaces(string latitude, string longitude, double radius, string apiKey,
            string type = "", string keyword = "", string region = "PL", string language = "pl", bool loadAllPages = false)
        {
            var latitudeString = latitude.ToString(CultureInfo.InvariantCulture);
            var longitudeString = longitude.ToString(CultureInfo.InvariantCulture);
            var listPlaces = new List<Place>();
            try
            {
                var webRequest =
                    WebRequest.Create(
                        @$"https://maps.googleapis.com/maps/api/place/nearbysearch/json?location={latitudeString},{longitudeString}&radius={radius}&type={type}&keyword={keyword}&region={region}&language={language}&key={apiKey}");
                var response = ConvertApiJsonToString(webRequest);
                Status status;
                Enum.TryParse(TrimJson(response, "status"), out status);
                if (status != Status.OK)
                {
                    return listPlaces;
                }
                var trimmedJson = TrimJson(response, "results");
                var nextPageToken = TrimJson(response, "next_page_token");

                listPlaces.AddRange(JsonConvert.DeserializeObject<List<Place>>(trimmedJson));


                if (!string.IsNullOrEmpty(nextPageToken) && loadAllPages)
                {
                    Thread.Sleep(1000);
                    webRequest =
                        WebRequest.Create(
                            @$"https://maps.googleapis.com/maps/api/place/nearbysearch/json?pagetoken={nextPageToken}&key={apiKey}");
                    response = ConvertApiJsonToString(webRequest);

                    Enum.TryParse(TrimJson(response, "status"), out status);
                    if (status != Status.OK)
                    {
                        return listPlaces;
                    }
                    trimmedJson = TrimJson(response, "results");
                    nextPageToken = TrimJson(response, "next_page_token");
                    listPlaces.AddRange(JsonConvert.DeserializeObject<List<Place>>(trimmedJson));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return listPlaces;
        }

        public static List<Place> ApiSearchPlaces(string apiKey, string keyword = "", string region = "PL", string language = "pl")
        {
            var listPlaces = new List<Place>();
            try
            {
                var webRequest =
                    WebRequest.Create(
                        @$"https://maps.googleapis.com/maps/api/place/textsearch/json?query={keyword}&key={apiKey}");
                var response = ConvertApiJsonToString(webRequest);
                Status status;
                Enum.TryParse(TrimJson(response, "status"), out status);
                if (status != Status.OK)
                {
                    return listPlaces;
                }
                var trimmedJson = TrimJson(response, "results");

                return JsonConvert.DeserializeObject<List<Place>>(trimmedJson);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    enum Status
    {
        OK,
        ZERO_RESULTS,
        INVALID_REQUEST,
        OVER_QUERY_LIMIT,
        REQUEST_DENIED,
        UNKNOWN_ERROR
    }
}

